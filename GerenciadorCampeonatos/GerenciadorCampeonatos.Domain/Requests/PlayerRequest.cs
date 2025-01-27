using GerenciadorCampeonatos.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorCampeonatos.Domain.Requests;

public class PlayerRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Player name must be between 2 and 100 characters long.")]
    public string Name { get; set; }

    [Required]
    [PlayerPosition]
    public string Position { get; set; }

    [Required]
    [Range(15, 50, ErrorMessage = "The player must be between 15 and 50 years old.")]
    public int Age { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The team id must be valid.")]
    public int TeamId { get; set; }
}
