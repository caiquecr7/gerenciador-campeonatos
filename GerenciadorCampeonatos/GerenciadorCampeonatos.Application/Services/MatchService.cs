using GerenciadorCampeonatos.Domain.Database;
using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Requests;
using GerenciadorCampeonatos.Domain.Requests.MatchRequests;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCampeonatos.Application.Services;

public class MatchService : IMatchService
{
    private readonly CampeonatosDbContext _context;

    public MatchService(CampeonatosDbContext context)
    {
        _context = context;
    }

    public async Task<Match> Create(IncludeMatchRequest matchModel)
    {
        await ValidateTeamsInRequest(matchModel);

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

    public async Task<bool> Update(int id, UpdateMatchRequest updatedMatch)
    {
        var existingMatch = await _context.Matches.FindAsync(id);
        if (existingMatch == null)
            return false;

        await ValidateTeamsInRequest(updatedMatch);

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

    private async Task ValidateTeamsInRequest(MatchRequest request)
    {
        var homeTeamExists = await _context.Teams.AnyAsync(t => t.Id == request.HomeTeamId);
        if (!homeTeamExists)
            throw new ArgumentException($"The home team linked to the match does not exist");

        var awayTeamExists = await _context.Teams.AnyAsync(t => t.Id == request.AwayTeamId);
        if (!awayTeamExists)
            throw new ArgumentException($"The away team linked to the match does not exist");
    }
}
