namespace GerenciadorCampeonatos.Domain.Entities
{
    public class Match : Entity
    {
        public int GoalsHomeTeam { get; set; }
        public int GoalsAwayTeam { get; set; }
        public DateTime Date { get; set; }
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public ICollection<Player> Players { get; set; } = [];
    }
}
