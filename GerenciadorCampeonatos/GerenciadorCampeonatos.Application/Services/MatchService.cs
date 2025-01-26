using GerenciadorCampeonatos.Domain.Database;
using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Models.MatchModels;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCampeonatos.Application.Services;

public class MatchService : IMatchService
{
    private readonly CampeonatosDbContext _context;

    public MatchService(CampeonatosDbContext context)
    {
        _context = context;
    }

    public async Task<Match> Create(IncludeMatchModel matchModel)
    {
        var match = matchModel.ToEntity();

        _context.Matches.Add(match);
        await _context.SaveChangesAsync();

        return match;
    }

    public async Task<Match> GetById(int id)
    {
        return await _context.Matches.FindAsync(id);
    }

    public async Task<List<Match>> GetAll()
    {
        return await _context.Matches.ToListAsync();
    }

    public async Task<bool> Update(int id, UpdateMatchModel updatedMatch)
    {
        var existingMatch = await _context.Matches.FindAsync(id);
        if (existingMatch == null)
            return false;

        updatedMatch.UpdateEntity(existingMatch);

        _context.Matches.Update(existingMatch);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var match = await _context.Matches.FindAsync(id);
        if (match == null)
            return false;

        _context.Matches.Remove(match);
        await _context.SaveChangesAsync();

        return true;
    }
}
