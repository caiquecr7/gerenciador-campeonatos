using GerenciadorCampeonatos.Domain.Results;

namespace GerenciadorCampeonatos.Domain.Requests.MatchRequests;

public class SearchMatchRequest : PagedSearchModel
{
    public int? GoalsHomeTeam { get; set; }
    public int? GoalsAwayTeam { get; set; }
    public DateTime? Date { get; set; }
    public int? HomeTeamId { get; set; }
    public int? AwayTeamId { get; set; }
}
