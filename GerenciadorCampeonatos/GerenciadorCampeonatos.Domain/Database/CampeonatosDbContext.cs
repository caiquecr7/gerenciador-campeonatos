using GerenciadorCampeonatos.Domain.Database.Maps;
using GerenciadorCampeonatos.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCampeonatos.Domain.Database
{
    public class CampeonatosDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        public CampeonatosDbContext(DbContextOptions<CampeonatosDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("futebol");

            modelBuilder.ApplyConfiguration(new TeamMap());
            modelBuilder.ApplyConfiguration(new MatchMap());
            modelBuilder.ApplyConfiguration(new PlayerMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
