using System.ComponentModel;

namespace MovieCatalog.Domain.Extensions;

/// <summary>
/// Helper class to work with enums
/// </summary>
public static class EnumHelper
{
	/// <summary>
	/// Gets an attribute on an enum field value
	/// </summary>
	/// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
	/// <param name="enumVal">The enum value</param>
	/// <returns>The attribute of type T that exists on the enum value</returns>
	/// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
	public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
	{
		var type = enumVal.GetType();
		var memInfo = type.GetMember(enumVal.ToString());
		var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
		return attributes.Length > 0 ? (T)attributes[0] : null;
	}

	/// <summary>
	/// Returns the description of an enum value as string for display purposes
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string GetEnumDescription(this Enum value)
	{
		System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());

		DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

		if (attributes != null && attributes.Length > 0)
			return attributes[0].Description;
		else
			return value.ToString();
	}
}