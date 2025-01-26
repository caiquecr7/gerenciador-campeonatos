using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Models.PlayerModels;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface IPlayerService
{
    Task<Player> Create(IncludePlayerModel playerModel);
    Task<Player> GetById(int id);
    Task<List<Player>> GetAll();
    Task<bool> Update(int id, UpdatePlayerModel updatedPlayer);
    Task<bool> Delete(int id);
}
