using System.ComponentModel;

namespace MovieCatalog.Domain.Enums;

public enum Status
{
	[Description("Upcoming")]
	Upcoming,
	[Description("Released")]
	Released
}
