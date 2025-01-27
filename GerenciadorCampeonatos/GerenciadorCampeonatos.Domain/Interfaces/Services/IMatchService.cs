using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Requests.MatchRequests;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface IMatchService
{
    Task<Match> Create(IncludeMatchRequest matchModel);
    Task<Match> GetById(int id);
    Task<List<Match>> GetAll();
    Task<bool> Update(int id, UpdateMatchRequest updatedMatch);
    Task<bool> Delete(int id);
}
