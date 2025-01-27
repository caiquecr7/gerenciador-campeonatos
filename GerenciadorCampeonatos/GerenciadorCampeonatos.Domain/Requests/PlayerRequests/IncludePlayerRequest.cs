using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.ValueObjects;

namespace GerenciadorCampeonatos.Domain.Requests.PlayerRequests;

public class IncludePlayerRequest : PlayerRequest
{
    public Player ToEntity()
    {
        var valuePosition = PlayerPosition.TryParse(Position);
        return new Player()
        {
            Name = Name,
            Position = valuePosition.Value,
            Age = Age,
            TeamId = TeamId
        };
    }
}
