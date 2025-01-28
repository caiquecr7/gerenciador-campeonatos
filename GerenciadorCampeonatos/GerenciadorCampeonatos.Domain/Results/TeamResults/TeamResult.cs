using GerenciadorCampeonatos.Domain.Entities;

namespace GerenciadorCampeonatos.Domain.Results.TeamResults;

public class TeamResult
{
    public string Name { get; set; }
    public string City { get; set; }
    public int FoundationYear { get; set; }

    public static TeamResult FromEntity(Team team)
    {
        return new TeamResult
        {
            Name = team.Name,
            City = team.City,
            FoundationYear = team.FoundationYear
        };
    }
}
