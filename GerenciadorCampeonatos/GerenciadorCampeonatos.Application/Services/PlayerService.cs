using GerenciadorCampeonatos.Domain.Database;
using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Models.PlayerModels;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCampeonatos.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly CampeonatosDbContext _context;

    public PlayerService(CampeonatosDbContext context)
    {
        _context = context;
    }

    public async Task<Player> Create(IncludePlayerModel playerModel)
    {
        var player = playerModel.ToEntity();

        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        return player;
    }

    public async Task<Player> GetById(int id)
    {
        return await _context.Players.FindAsync(id);
    }

    public async Task<List<Player>> GetAll()
    {
        return await _context.Players.ToListAsync();
    }

    public async Task<bool> Update(int id, UpdatePlayerModel updatedPlayer)
    {
        var existingPlayer = await _context.Players.FindAsync(id);
        if (existingPlayer == null)
            return false;

        updatedPlayer.UpdateEntity(existingPlayer);

        _context.Players.Update(existingPlayer);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
            return false;

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();

        return true;
    }
}
