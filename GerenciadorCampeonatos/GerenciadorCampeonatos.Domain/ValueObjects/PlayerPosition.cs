namespace GerenciadorCampeonatos.Domain.ValueObjects
{
    public record PlayerPosition
    {
        public static readonly PlayerPosition Goalkeeper = new(GoalkeeperCode);
        private const string GoalkeeperCode = "GK";
        private const string GoalkeeperDescription = "Goalkeeper";

        public static readonly PlayerPosition Defender = new(DefenderCode);
        private const string DefenderCode = "DF";
        private const string DefenderDescription = "Defender";

        public static readonly PlayerPosition Midfielder = new(MidfielderCode);
        private const string MidfielderCode = "MF";
        private const string MidfielderDescription = "Midfielder";

        public static readonly PlayerPosition Striker = new(StrikerCode);
        private const string StrikerCode = "ST";
        private const string StrikerDescription = "Striker";

        public string Code { get; init; }

        public string Description => Code switch
        {
            GoalkeeperCode => GoalkeeperDescription,
            DefenderCode => DefenderDescription,
            MidfielderCode => MidfielderDescription,
            StrikerCode => StrikerDescription,
            _ => string.Empty
        };

        private PlayerPosition() { }
        private PlayerPosition(string code)
        {
            Code = code;
        }

        public override string ToString() => Description;

        public static implicit operator string(PlayerPosition position) => position.ToString();
    }
}
