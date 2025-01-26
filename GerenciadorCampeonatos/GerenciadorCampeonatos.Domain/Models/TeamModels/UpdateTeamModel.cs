using GerenciadorCampeonatos.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorCampeonatos.Domain.Models.TeamModels;

public class UpdateTeamModel
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome do time deve ter entre 2 e 100 caracteres.")]
    public string Name { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "A cidade deve ter entre 3 e 50 caracteres.")]
    public string City { get; set; }

    [Required]
    [Range(1800, 2100, ErrorMessage = "O ano de fundação deve ser válido.")]
    public int FoundationYear { get; set; }

    public void UpdateEntity(Team team)
    {
        team.Name = Name;
        team.City = City;
        team.FoundationYear = FoundationYear;
    }
}
