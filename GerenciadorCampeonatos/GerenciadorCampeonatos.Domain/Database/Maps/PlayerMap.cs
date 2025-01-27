using GerenciadorCampeonatos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorCampeonatos.Domain.Database.Maps;

public class PlayerMap : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Player");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").IsRequired();
        builder.Property(x => x.Age).HasColumnName("Age").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("Created_At").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("Updated_At");

        builder.ComplexProperty(e => e.Position, status =>
        {
            status.IsRequired();
            status.Property(p => p.Code).HasColumnName("Position").IsRequired();
        });

        builder.HasOne(e => e.Team)
            .WithMany(l => l.Players)
            .HasForeignKey(e => e.TeamId).HasConstraintName("FK_Player_team");

        builder.HasMany(s => s.Matches)
            .WithMany(c => c.Players)
            .UsingEntity<Dictionary<string, object>>(
                "MatchPlayer",
                l => l.HasOne<Match>().WithMany().HasForeignKey("Match_Id"),
                c => c.HasOne<Player>().WithMany().HasForeignKey("Player_Id"));
    }
}
