using GerenciadorCampeonatos.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorCampeonatos.WebApi.Models.TeamModels;

public class IncludeTeamModel
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Team name must be between 2 and 100 characters long.")]
    public string Name { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "The city must be between 3 and 50 characters long.")]
    public string City { get; set; }

    [Required]
    [Range(1800, 2100, ErrorMessage = "The year of foundation must be valid.")]
    public int FoundationYear { get; set; }

    public Team ToEntity()
    {
        return new Team()
        {
            Name = Name,
            City = City,
            FoundationYear = FoundationYear
        };
    }
}
