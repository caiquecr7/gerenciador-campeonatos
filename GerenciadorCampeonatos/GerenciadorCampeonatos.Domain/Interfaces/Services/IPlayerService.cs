using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Requests.PlayerRequests;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface IPlayerService
{
    Task<Player> Create(IncludePlayerRequest playerModel);
    Task<Player> GetById(int id);
    Task<List<Player>> GetAll();
    Task<bool> Update(int id, UpdatePlayerRequest updatedPlayer);
    Task<bool> Delete(int id);
}
