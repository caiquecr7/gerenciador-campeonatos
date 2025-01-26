using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Models.MatchModels;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface IMatchService
{
    Task<Match> Create(IncludeMatchModel matchModel);
    Task<Match> GetById(int id);
    Task<List<Match>> GetAll();
    Task<bool> Update(int id, UpdateMatchModel updatedMatch);
    Task<bool> Delete(int id);
}
