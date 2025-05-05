using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Converters;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string dateString = reader.GetString();
        
        // Try to parse using multiple formats
        if (DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime result))
        {
            return result;
        }

        // Fallback to default parsing
        return DateTime.Parse(dateString, null, DateTimeStyles.AssumeUniversal);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
    }
}