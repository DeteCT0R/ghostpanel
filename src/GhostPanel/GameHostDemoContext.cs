using GameHostDemo.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameHostDemo
{
    class GameHostDemoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\Barry\source\repos\ghostpanel\src\hosting.db");
        }

        public DbSet<Game> Games {get; set;}
        public DbSet<GameServer> GameServers { get; set; }
    }
}
