using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Models.TeamModels;
using GerenciadorCampeonatos.WebApi.Models.TeamModels;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface ITeamService
{
    Task<Team> Create(IncludeTeamModel teamModel);
    Task<Team> GetById(int id);
    Task<List<Team>> GetAll();
    Task<bool> Update(int id, UpdateTeamModel updatedTeam);
    Task<bool> Delete(int id);
}
