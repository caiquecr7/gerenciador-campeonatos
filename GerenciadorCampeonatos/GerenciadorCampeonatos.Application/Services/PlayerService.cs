using GerenciadorCampeonatos.Domain.Database;
using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Requests;
using GerenciadorCampeonatos.Domain.Requests.PlayerRequests;
using GerenciadorCampeonatos.Domain.Results;
using GerenciadorCampeonatos.Domain.Results.PlayerResults;
using GerenciadorCampeonatos.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GerenciadorCampeonatos.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly CampeonatosDbContext _context;

    public PlayerService(CampeonatosDbContext context)
    {
        _context = context;
    }

    public async Task<Player> Create(IncludePlayerRequest playerModel)
    {
        await ValidateTeamInRequest(playerModel);

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

    public async Task<bool> Update(int id, UpdatePlayerRequest updatedPlayer)
    {
        var existingPlayer = await _context.Players.FindAsync(id);
        if (existingPlayer == null)
            return false;

        await ValidateTeamInRequest(updatedPlayer);

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

    private async Task ValidateTeamInRequest(PlayerRequest request)
    {
        var teamExists = await _context.Teams.AnyAsync(t => t.Id == request.TeamId);
        if (!teamExists)
            throw new ArgumentException("The team linked to the player does not exist");
    }

    public async Task<PagedResult<PlayerResult>> Search(SearchPlayerRequest request)
    {
        var query = _context.Players
            .Include(x => x.Team)
            .AsQueryable();

        query = ApplyFilters(query, request);
        query = ApplyOrdening(query, request);

        var modelQuery = query.Select(player => PlayerResult.FromEntity(player, player.Team));
        var paginatedResult = await PagedResult<PlayerResult>.CreateAsync(modelQuery, request.Page, request.PageSize);

        return paginatedResult;
    }

    private IQueryable<Player> ApplyFilters(IQueryable<Player> query, SearchPlayerRequest request)
    {
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(t => t.Name.Contains(request.Name));

        if (!string.IsNullOrEmpty(request.Position))
        {
            var result = PlayerPosition.TryParse(request.Position);
            if(result.IsSuccess)
                query = query.Where(t => t.Position == result.Value);
        }
        
        if(request.Age.HasValue)
            query = query.Where(t => t.Age == request.Age);

        if (request.TeamId.HasValue)
            query = query.Where(t => t.TeamId == request.TeamId);

        return query;
    }

    private IQueryable<Player> ApplyOrdening(IQueryable<Player> query, SearchPlayerRequest request)
    {
        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            var propertyInfo = typeof(Player).GetProperty(request.OrderBy,
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
