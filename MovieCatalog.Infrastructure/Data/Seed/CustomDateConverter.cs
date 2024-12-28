using Newtonsoft.Json;
using System.Globalization;

namespace MovieCatalog.Infrastructure.Data.Seed;

/// <summary>
/// Custom date converter for JSON serialization, works with the format "dd.MM.yyyy"
/// </summary>
public class CustomDateConverter : JsonConverter
{
    private const string DateFormat = "dd.MM.yyyy";

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            if (objectType == typeof(DateTime?)) return null;
            throw new JsonSerializationException("Null value for non-nullable DateTime");
        }

        if (reader.TokenType == JsonToken.String)
        {
            var dateString = reader.Value.ToString();
            if (DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }
            throw new JsonSerializationException($"Invalid date format. Expected format: {DateFormat}");
        }

        throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is DateTime date)
        {
            writer.WriteValue(date.ToString(DateFormat));
        }
        else
        {
            throw new JsonSerializationException("Expected DateTime object value");
        }
    }
}