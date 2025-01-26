using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.WebApi.Models.TeamModels;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface ITeamService
{
    Task<Team> Create(IncludeTeamModel teamModel);
    Task<Team> GetById(int id);
}
