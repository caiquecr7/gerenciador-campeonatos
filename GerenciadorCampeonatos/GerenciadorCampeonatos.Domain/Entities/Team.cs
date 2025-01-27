namespace GerenciadorCampeonatos.Domain.Entities
{
    public class Team : Entity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public int FoundationYear { get; set; }

        public ICollection<Player> Players { get; set; } = [];
        public ICollection<Match> HomeMatches { get; set; } = [];
        public ICollection<Match> AwayMatches { get; set; } = [];
    }
}
