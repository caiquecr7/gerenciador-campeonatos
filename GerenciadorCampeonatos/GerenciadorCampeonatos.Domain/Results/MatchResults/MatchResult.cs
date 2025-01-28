using GerenciadorCampeonatos.Domain.Entities;

namespace GerenciadorCampeonatos.Domain.Results.MatchResults;

public class MatchResult
{
    public int GoalsHomeTeam { get; set; }
    public int GoalsAwayTeam { get; set; }
    public DateTime Date { get; set; }
    public string HomeTeamName { get; set; }
    public string AwayTeamName { get; set; }

    public static MatchResult FromEntity(Match match)
    {
        return new MatchResult
        {
            GoalsHomeTeam = match.GoalsHomeTeam,
            GoalsAwayTeam = match.GoalsAwayTeam,
            Date = match.Date,
            HomeTeamName = match.HomeTeam.Name,
            AwayTeamName = match.AwayTeam.Name
        };
    }
}
