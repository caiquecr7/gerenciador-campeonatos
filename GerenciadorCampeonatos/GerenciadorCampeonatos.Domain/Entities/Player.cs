using GerenciadorCampeonatos.Domain.ValueObjects;

namespace GerenciadorCampeonatos.Domain.Entities
{
    public class Player : Entity
    {
        public string Name { get; set; }
        public PlayerPosition Position { get; set; }
        public DateTime BirthDate { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public ICollection<Match> Matches { get; set; } = [];
    }
}
