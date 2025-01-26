using GerenciadorCampeonatos.Domain.Entities;

namespace GerenciadorCampeonatos.Domain.Requests.MatchRequests;

public class IncludeMatchRequest : MatchRequest
{
    public Match ToEntity()
    {
        return new Match()
        {
            GoalsHomeTeam = GoalsHomeTeam,
            GoalsAwayTeam = GoalsAwayTeam,
            Date = Date,
            HomeTeamId = HomeTeamId,
            AwayTeamId = AwayTeamId
        };
    }
}
