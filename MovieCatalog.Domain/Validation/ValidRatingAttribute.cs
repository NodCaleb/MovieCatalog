using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Domain.Validation;

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