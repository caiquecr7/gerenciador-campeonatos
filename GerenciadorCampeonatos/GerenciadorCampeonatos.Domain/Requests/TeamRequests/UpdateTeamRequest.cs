using GerenciadorCampeonatos.Domain.Entities;

namespace GerenciadorCampeonatos.Domain.Requests.TeamRequests;

public class UpdateTeamRequest : TeamRequest
{
    public void UpdateEntity(Team team)
    {
        team.Name = Name;
        team.City = City;
        team.FoundationYear = FoundationYear;
        team.UpdatedAt = DateTime.Now;
    }
}
