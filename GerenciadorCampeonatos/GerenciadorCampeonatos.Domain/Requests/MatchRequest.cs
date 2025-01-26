using System.ComponentModel.DataAnnotations;

namespace GerenciadorCampeonatos.Domain.Requests;

public class MatchRequest
{
    [Required]
    [Range(0, 50, ErrorMessage = "The goals from home team must be between 0 and 50 goals.")]
    public int GoalsHomeTeam { get; set; }

    [Required]
    [Range(0, 50, ErrorMessage = "The goals from away team must be between 0 and 50 goals.")]
    public int GoalsAwayTeam { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The home team id must be valid.")]
    public int HomeTeamId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The away team id must be valid.")]
    public int AwayTeamId { get; set; }
}
