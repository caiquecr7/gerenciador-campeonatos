using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Requests.PlayerRequests;
using GerenciadorCampeonatos.Domain.Results;
using GerenciadorCampeonatos.Domain.Results.PlayerResults;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface IPlayerService
{
    Task<Player> Create(IncludePlayerRequest playerModel);
    Task<Player> GetById(int id);
    Task<List<Player>> GetAll();
    Task<bool> Update(int id, UpdatePlayerRequest updatedPlayer);
    Task<bool> Delete(int id);
    Task<PagedResult<PlayerResult>> Search(SearchPlayerRequest searchModel);
}
