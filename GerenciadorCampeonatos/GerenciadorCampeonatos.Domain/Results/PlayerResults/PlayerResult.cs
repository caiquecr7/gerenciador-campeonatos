using GerenciadorCampeonatos.Domain.Entities;

namespace GerenciadorCampeonatos.Domain.Results.PlayerResults
{
    public class PlayerResult
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public string TeamName { get; set; }

        public static PlayerResult FromEntity(Player player, Team team)
        {
            return new PlayerResult
            {
                Name = player.Name,
                Position = player.Position.Code,
                Age = player.Age,
                TeamName = team.Name
            };
        }
    }
}
