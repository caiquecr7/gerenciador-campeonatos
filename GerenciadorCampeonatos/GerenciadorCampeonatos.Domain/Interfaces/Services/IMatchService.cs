using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.Requests.MatchRequests;
using GerenciadorCampeonatos.Domain.Results;
using GerenciadorCampeonatos.Domain.Results.MatchResults;

namespace GerenciadorCampeonatos.Domain.Interfaces.Services;

public interface IMatchService
{
    Task<Match> Create(IncludeMatchRequest matchModel);
    Task<Match> GetById(int id);
    Task<List<Match>> GetAll();
    Task<bool> Update(int id, UpdateMatchRequest updatedMatch);
    Task<bool> Delete(int id);
    Task<PagedResult<MatchResult>> Search(SearchMatchRequest searchModel);
}
