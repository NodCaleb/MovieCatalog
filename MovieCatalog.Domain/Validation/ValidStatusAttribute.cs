using MovieCatalog.Domain.Enums;
using MovieCatalog.Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Domain.Validation;

/// <summary>
/// Custom validation attribute to validate the status of a movie
/// Status must be in the list of allowed statuses
/// </summary>
public class ValidStatusAttribute : ValidationAttribute
{
	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		var allowedStatuses = Enum.GetValues(typeof(Status))
			.Cast<Status>() // Converts to IEnumerable<Status>
			.Select(s => s.GetEnumDescription()).ToList();

		if (value is string status && allowedStatuses.AsQueryable().Cast<string>().Contains(status.ToString()))
		{
			return ValidationResult.Success;
		}

		return new ValidationResult($"Status '{value}' is not valid, possible values: {string.Join(", ", allowedStatuses)}");
	}
}