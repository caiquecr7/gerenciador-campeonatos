using GerenciadorCampeonatos.Domain.Database;
using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Requests.TeamRequests;
using GerenciadorCampeonatos.Domain.Results;
using GerenciadorCampeonatos.Domain.Results.TeamResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GerenciadorCampeonatos.Application.Services;

public class TeamService : ITeamService
{
    private readonly CampeonatosDbContext _context;

    public TeamService(CampeonatosDbContext context)
    {
        _context = context;
    }

    public async Task<Team> Create(IncludeTeamRequest teamModel)
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

    public async Task<bool> Update(int id, UpdateTeamRequest updatedTeam)
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

    public async Task<PagedResult<TeamResult>> Search(SearchTeamRequest request)
    {
        var query = _context.Teams.AsQueryable();

        query = ApplyFilters(query, request);
        query = ApplyOrdening(query, request);

        var modelQuery = query.Select(team => TeamResult.FromEntity(team));
        var paginatedResult = await PagedResult<TeamResult>.CreateAsync(modelQuery, request.Page, request.PageSize);

        return paginatedResult;
    }

    private IQueryable<Team> ApplyFilters(IQueryable<Team> query, SearchTeamRequest request)
    {
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(t => t.Name.Contains(request.Name));

        if (!string.IsNullOrEmpty(request.City))
            query = query.Where(t => t.City.Contains(request.City));

        if (request.FoundationYear.HasValue)
            query = query.Where(t => t.FoundationYear == request.FoundationYear);

        return query;
    }

    private IQueryable<Team> ApplyOrdening(IQueryable<Team> query, SearchTeamRequest request)
    {
        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            var propertyInfo = typeof(Team).GetProperty(request.OrderBy,
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance);

            if (propertyInfo != null)
            {
                query = request.OrderByAscending
                    ? query.OrderBy(e => EF.Property<object>(e, propertyInfo.Name))
                    : query.OrderByDescending(e => EF.Property<object>(e, propertyInfo.Name));
            }
        }

        return query;
    }
}
