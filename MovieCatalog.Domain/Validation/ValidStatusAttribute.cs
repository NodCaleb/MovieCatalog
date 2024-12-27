using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Domain.Validation;

public class ValidStatusAttribute : ValidationAttribute
{
	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		var allowedStatuses = new[] { "Upcoming", "Released" };

		if (value is string status && allowedStatuses.Contains(status.ToString()))
		{
			return ValidationResult.Success;
		}

		return new ValidationResult($"Status '{value}' is not valid, possible values: {string.Join(", ", allowedStatuses)}");
	}
}