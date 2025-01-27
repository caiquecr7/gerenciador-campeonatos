using GerenciadorCampeonatos.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCampeonatos.Domain.Database.Maps;

public class TeamMap : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Team");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").IsRequired();
        builder.Property(x => x.City).HasColumnName("City").IsRequired();
        builder.Property(x => x.FoundationYear).HasColumnName("Foundation_Year").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("Created_At").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("Updated_At");

        builder.HasMany(e => e.Players)
            .WithOne(l => l.Team);
    }
}
