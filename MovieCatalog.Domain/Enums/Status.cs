using System.ComponentModel;

namespace MovieCatalog.Domain.Enums;

/// <summary>
/// Enum to represent the status of a movie if it has been released or not
/// </summary>
public enum Status
{
	[Description("Upcoming")]
	Upcoming,
	[Description("Released")]
	Released,
	[Description("Cancelled")]
	Cancelled
}
