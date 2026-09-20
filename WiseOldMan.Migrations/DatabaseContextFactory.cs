using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Protocols.Configuration;
using WiseOldMan.Bot.Data;

namespace WiseOldMan.Migrations;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var connectionString =
            args.FirstOrDefault()
            ?? throw new InvalidConfigurationException(
                "Please provide a valid connection string for the Migrations to run!"
            );

        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlServer(
                connectionString,
                sqlServer =>
                    sqlServer.MigrationsAssembly(
                        typeof(DatabaseContextFactory).Assembly.GetName().Name
                    )
            )
            .Options;

        return new DatabaseContext(options);
    }
}
