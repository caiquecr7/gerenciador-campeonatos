using FluentResults;

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

        public static readonly PlayerPosition NotDefined = new(NotDefinedCode);
        private const string NotDefinedCode = "";
        private const string NotDefinedDescription = "Not Defined";

        public string Code { get; init; }

        public string Description => Code switch
        {
            GoalkeeperCode => GoalkeeperDescription,
            DefenderCode => DefenderDescription,
            MidfielderCode => MidfielderDescription,
            StrikerCode => StrikerDescription,
            NotDefinedCode => NotDefinedCode,
            _ => string.Empty
        };

        private PlayerPosition() { }
        private PlayerPosition(string code)
        {
            Code = code;
        }

        public override string ToString() => Description;

        public static implicit operator string(PlayerPosition position) => position.ToString();

        public static Result<PlayerPosition> TryParse(string codigo)
        {
            return codigo switch
            {
                GoalkeeperCode => Goalkeeper,
                GoalkeeperDescription => Goalkeeper,
                DefenderCode => Defender,
                DefenderDescription => Defender,
                MidfielderCode => Midfielder,
                MidfielderDescription => Midfielder,
                StrikerCode => Striker,
                StrikerDescription => Striker,
                _ => NotDefined
            };
        }
    }
}
