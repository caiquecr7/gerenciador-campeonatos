using GerenciadorCampeonatos.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCampeonatos.Domain.Database.Maps;

public class MatchMap : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("Match");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
        builder.Property(x => x.GoalsHomeTeam).HasColumnName("Goals_Home_Team").IsRequired();
        builder.Property(x => x.GoalsAwayTeam).HasColumnName("Goals_Away_Team").IsRequired();
        builder.Property(x => x.Date).HasColumnName("Date").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("Created_At").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("Updated_At");

        builder.HasOne(m => m.HomeTeam)
            .WithMany(t => t.HomeMatches)
            .HasForeignKey(m => m.HomeTeamId)
            .HasConstraintName("FK_HomeTeam_Match")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.AwayTeam)
            .WithMany(t => t.AwayMatches)
            .HasForeignKey(m => m.AwayTeamId)
            .HasConstraintName("FK_AwayTeam_Match")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
