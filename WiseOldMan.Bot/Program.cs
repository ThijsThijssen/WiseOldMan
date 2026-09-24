using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WiseOldMan.Bot.Data;
using WiseOldMan.Bot.Handlers;
using WiseOldMan.Bot.Services;

namespace WiseOldMan.Bot;

public class Program
{
    private static IConfiguration _configuration;
    private static IServiceProvider _services;

    private static readonly DiscordSocketConfig _socketConfig = new()
    {
        GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
        AlwaysDownloadUsers = true,
    };

    private static readonly InteractionServiceConfig _interactionServiceConfig = new() { };

    public static async Task Main(string[] args)
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        var connectionString =
            _configuration.GetConnectionString("DatabaseContext")
            ?? throw new InvalidOperationException(
                "Connection string 'DatabaseContext' not found."
            );

        _services = new ServiceCollection()
            .AddSingleton(_configuration)
            .AddSingleton(_socketConfig)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton(x => new InteractionService(
                x.GetRequiredService<DiscordSocketClient>(),
                _interactionServiceConfig
            ))
            .AddSingleton<InteractionHandler>()
            .AddSingleton<ModalHandler>()
            .AddSingleton<PlayerService>()
            .AddPooledDbContextFactory<DatabaseContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    x => x.MigrationsAssembly("WiseOldMan.Migrations")
                )
            )
            .BuildServiceProvider();

        // Ensure database is created
        using (DatabaseContext context = _services.GetRequiredService<DatabaseContext>())
        {
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context);
        }

        var client = _services.GetRequiredService<DiscordSocketClient>();
        var modalHandler = _services.GetRequiredService<ModalHandler>();

        client.Log += LogAsync;
        client.ModalSubmitted += modalHandler.HandleAsync;

        // Here we can initialize the service that will register and execute our commands
        await _services.GetRequiredService<InteractionHandler>().InitializeAsync();

        // Bot token can be provided from the Configuration object we set up earlier
        await client.LoginAsync(TokenType.Bot, _configuration["token"]);
        await client.StartAsync();

        // Never quit the program until manually forced to.
        await Task.Delay(Timeout.Infinite);
    }

    private static Task LogAsync(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }
}
