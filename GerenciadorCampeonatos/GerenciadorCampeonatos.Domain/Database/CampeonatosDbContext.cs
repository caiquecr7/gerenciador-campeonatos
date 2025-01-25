using GerenciadorCampeonatos.Domain.Database.Maps;
using GerenciadorCampeonatos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCampeonatos.Domain.Database
{
    public class CampeonatosDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        public CampeonatosDbContext(DbContextOptions<CampeonatosDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TeamMap());
            modelBuilder.ApplyConfiguration(new MatchMap());
            modelBuilder.ApplyConfiguration(new PlayerMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
