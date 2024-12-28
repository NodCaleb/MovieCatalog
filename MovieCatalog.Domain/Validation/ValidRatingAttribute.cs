using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Domain.Validation;

/// <summary>
/// Custom validation attribute to validate the rating of a movie
/// Rating must be between 0 and 10
/// </summary>
public class ValidRatingAttribute : ValidationAttribute
{
	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		var minValue = 0.0f;
		var maxValue = 10.0f;

		if (value is float rating && rating >= minValue && rating <= maxValue)
		{
			return ValidationResult.Success;
		}

		return new ValidationResult($"Rating '{value}' is not valid, possible values: from {minValue} to {maxValue}");
	}
}