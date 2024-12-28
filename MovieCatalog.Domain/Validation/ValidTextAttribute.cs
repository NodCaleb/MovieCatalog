using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Domain.Validation;

/// <summary>
/// Custom validation attribute to validate text fields
/// Checks if the value is required and if it exceeds the max length
/// </summary>
public class ValidTextAttribute : ValidationAttribute
{
	private readonly bool _isRequired;
	private readonly int? _maxLength;

	public ValidTextAttribute(bool isRequired, int maxLength)
	{
		_isRequired = isRequired;
		_maxLength = maxLength;
	}

	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		// Check if the value is required
		if (_isRequired && (value == null || string.IsNullOrWhiteSpace(value.ToString())))
		{
			return new ValidationResult($"{validationContext.MemberName} is required.");
		}

		// Check if the value exceeds the max length
		if (_maxLength.HasValue && value != null)
		{
			var stringValue = value.ToString();
			if (stringValue.Length > _maxLength.Value)
			{
				return new ValidationResult($"{validationContext.MemberName} must not exceed {_maxLength.Value} characters.");
			}
		}

		return ValidationResult.Success;
	}
}
