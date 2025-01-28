using GerenciadorCampeonatos.Domain.Results;

namespace GerenciadorCampeonatos.Domain.Requests.PlayerRequests;

public class SearchPlayerRequest : PagedSearchModel
{
    public string? Name { get; set; }
    public string? Position { get; set; }
    public int? Age { get; set; }
    public int? TeamId { get; set; }
}