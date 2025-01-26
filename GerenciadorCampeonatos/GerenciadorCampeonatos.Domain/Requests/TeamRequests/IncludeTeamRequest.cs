using GerenciadorCampeonatos.Domain.Entities;

namespace GerenciadorCampeonatos.Domain.Requests.TeamRequests;

public class IncludeTeamRequest : TeamRequest
{
    public Team ToEntity()
    {
        return new Team()
        {
            Name = Name,
            City = City,
            FoundationYear = FoundationYear
        };
    }
}
