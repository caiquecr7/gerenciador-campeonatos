using GerenciadorCampeonatos.Domain.Database;
using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Models.TeamModels;
using GerenciadorCampeonatos.WebApi.Models.TeamModels;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCampeonatos.Application.Services;

public class TeamService : ITeamService
{
    private readonly CampeonatosDbContext _context;

    public TeamService(CampeonatosDbContext context)
    {
        _context = context;
    }

    public async Task<Team> Create(IncludeTeamModel teamModel)
    {
        var team = teamModel.ToEntity();

        _context.Teams.Add(team);
        await _context.SaveChangesAsync();

        return team;
    }

    public async Task<Team> GetById(int id)
    {
        return await _context.Teams.FindAsync(id);
    }

    public async Task<List<Team>> GetAll()
    {
        return await _context.Teams.ToListAsync();
    }

    public async Task<bool> Update(int id, UpdateTeamModel updatedTeam)
    {
        var existingTeam = await _context.Teams.FindAsync(id);
        if (existingTeam == null)
            return false;

        updatedTeam.UpdateEntity(existingTeam);

        _context.Teams.Update(existingTeam);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team == null)
            return false;

        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();

        return true;
    }
}
