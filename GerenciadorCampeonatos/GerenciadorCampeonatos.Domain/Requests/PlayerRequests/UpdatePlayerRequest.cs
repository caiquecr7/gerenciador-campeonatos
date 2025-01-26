using GerenciadorCampeonatos.Domain.Entities;
using GerenciadorCampeonatos.Domain.ValueObjects;

namespace GerenciadorCampeonatos.Domain.Requests.PlayerRequests;

public class UpdatePlayerRequest : PlayerRequest
{
    public void UpdateEntity(Player player)
    {
        player.Name = Name;
        player.Position = PlayerPosition.TryParse(Position).Value;
        player.Age = Age;
        player.TeamId = TeamId;
        player.UpdatedAt = DateTime.Now;
    }
}
