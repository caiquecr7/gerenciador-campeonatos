using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Requests.MatchRequests;
using GerenciadorCampeonatos.Domain.Results;
using GerenciadorCampeonatos.Domain.Results.MatchResults;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface IMatchService
{
    Task<Match> Create(IncludeMatchRequest matchModel);
    Task<MatchResult> GetById(int id);
    Task<List<MatchResult>> GetAll();
    Task<bool> Update(int id, UpdateMatchRequest updatedMatch);
    Task<bool> Delete(int id);
    Task<PagedResult<MatchResult>> Search(SearchMatchRequest searchModel);
}
