using GerenciadorCampeonatos.Domain.Database;
using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Requests;
using GerenciadorCampeonatos.Domain.Requests.MatchRequests;
using GerenciadorCampeonatos.Domain.Results;
using GerenciadorCampeonatos.Domain.Results.MatchResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

    public async Task<MatchResult> GetById(int id)
    {
        var match = await _context.Matches
            .Include(x => x.HomeTeam)
            .Include(x => x.AwayTeam)
            .FirstAsync(x => x.Id == id);

        return MatchResult.FromEntity(match);
    }

    public async Task<List<MatchResult>> GetAll()
    {
        var match = await _context.Matches
            .Include(match => match.HomeTeam)
            .Include(match => match.AwayTeam)
            .ToListAsync();

        return match.Select(m => MatchResult.FromEntity(m)).ToList();
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

    public async Task<PagedResult<MatchResult>> Search(SearchMatchRequest request)
    {
        var query = _context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .AsQueryable();

        query = ApplyFilters(query, request);
        query = ApplyOrdening(query, request);

        var modelQuery = query.Select(match => MatchResult.FromEntity(match));
        var paginatedResult = await PagedResult<MatchResult>.CreateAsync(modelQuery, request.Page, request.PageSize);

        return paginatedResult;
    }

    private IQueryable<Match> ApplyFilters(IQueryable<Match> query, SearchMatchRequest request)
    {
        if(request.HomeTeamId.HasValue)
            query = query.Where(t => t.HomeTeamId == request.HomeTeamId);

        if(request.AwayTeamId.HasValue)
            query = query.Where(t => t.AwayTeamId == request.AwayTeamId);

        if(request.Date.HasValue)
            query = query.Where(t => t.Date == request.Date);

        if(request.GoalsHomeTeam.HasValue)
            query = query.Where(t => t.GoalsHomeTeam == request.GoalsHomeTeam);

        if (request.GoalsAwayTeam.HasValue)
            query = query.Where(t => t.GoalsAwayTeam == request.GoalsAwayTeam);

        return query;
    }

    private IQueryable<Match> ApplyOrdening(IQueryable<Match> query, SearchMatchRequest request)
    {
        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            var propertyInfo = typeof(Match).GetProperty(request.OrderBy,
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
