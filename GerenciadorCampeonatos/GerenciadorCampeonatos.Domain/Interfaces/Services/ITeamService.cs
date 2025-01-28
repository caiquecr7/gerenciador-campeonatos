using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Requests.TeamRequests;
using GerenciadorCampeonatos.Domain.Results;
using GerenciadorCampeonatos.Domain.Results.TeamResults;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface ITeamService
{
    Task<Team> Create(IncludeTeamRequest teamModel);
    Task<Team> GetById(int id);
    Task<List<Team>> GetAll();
    Task<bool> Update(int id, UpdateTeamRequest updatedTeam);
    Task<bool> Delete(int id);
    Task<PagedResult<TeamResult>> Search(SearchTeamRequest searchModel);
}
