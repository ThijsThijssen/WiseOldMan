using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WiseOldMan.Bot.Models;

namespace WiseOldMan.Bot.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<PlayerItem> PlayerItems { get; set; }
    public DbSet<PlayerSkill> PlayerSkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>().ToTable(nameof(Player));
        modelBuilder.Entity<Item>().ToTable(nameof(Item));
        modelBuilder.Entity<Skill>().ToTable(nameof(Skill));
        modelBuilder.Entity<PlayerItem>().ToTable(nameof(PlayerItem));
        modelBuilder.Entity<PlayerSkill>().ToTable(nameof(PlayerSkill));
    }
}
