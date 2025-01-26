using GerenciadorCampeonatos.Domain.Database;
using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.WebApi.Models.TeamModels;
using System.Web.Http;

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
}
