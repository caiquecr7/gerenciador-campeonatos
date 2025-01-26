using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Requests.TeamRequests;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface ITeamService
{
    Task<Team> Create(IncludeTeamRequest teamModel);
    Task<Team> GetById(int id);
    Task<List<Team>> GetAll();
    Task<bool> Update(int id, UpdateTeamRequest updatedTeam);
    Task<bool> Delete(int id);
}
