using GerenciadorCampeonatos.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorCampeonatos.Domain.Models.MatchModels;

public class UpdateMatchModel
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

    public void UpdateEntity(Match match)
    {
        match.GoalsHomeTeam = GoalsHomeTeam;
        match.GoalsAwayTeam = GoalsAwayTeam;
        match.Date = Date;
        match.HomeTeamId = HomeTeamId;
        match.AwayTeamId = AwayTeamId;
        match.UpdatedAt = DateTime.Now;
    }
}
