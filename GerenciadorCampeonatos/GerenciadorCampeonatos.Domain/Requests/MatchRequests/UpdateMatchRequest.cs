using GerenciadorCampeonatos.Domain.Entities;

namespace GerenciadorCampeonatos.Domain.Requests.MatchRequests;

public class UpdateMatchRequest : MatchRequest
{
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
