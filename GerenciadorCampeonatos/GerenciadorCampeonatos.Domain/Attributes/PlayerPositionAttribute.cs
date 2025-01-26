using GerenciadorCampeonatos.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorCampeonatos.Domain.Attributes
{
    public class PlayerPositionAttribute : ValidationAttribute
    {
        private static readonly PlayerPosition[] ValidPositions =
        {
            PlayerPosition.Goalkeeper,
            PlayerPosition.Defender,
            PlayerPosition.Midfielder,
            PlayerPosition.Striker
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult("The position is required.");

            string position = value.ToString();

            bool isValid = PlayerPosition.TryParse(position).IsSuccess;

            if (!isValid)
                return new ValidationResult($"Invalid position. Allowed values are: {string.Join(", ", ValidPositions.Select(p => $"{p.Code} ({p.Description})"))}");

            return ValidationResult.Success;
        }
    }
}
