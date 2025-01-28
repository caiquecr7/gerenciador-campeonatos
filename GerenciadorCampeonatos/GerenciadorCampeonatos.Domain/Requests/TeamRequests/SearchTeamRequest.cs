using GerenciadorCampeonatos.Domain.Results;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorCampeonatos.Domain.Requests.TeamRequests;

public class SearchTeamRequest : PagedSearchModel
{
    public string? Name { get; set; }
    public string? City { get; set; }
    [Range(1800, 2100)]
    public int? FoundationYear { get; set; }
}