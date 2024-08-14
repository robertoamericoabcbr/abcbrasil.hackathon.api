using Microsoft.Extensions.Configuration;

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ABCBrasil.Hackthon.Api.Infra.Providers.CultureProvider
{
    public class DateJsonConverter : JsonConverter<DateTime>
    {
        private string _shortDateTimePattern;

        public DateJsonConverter(IConfiguration configuration)
        {
            _shortDateTimePattern = configuration.GetSection("CultureProvider:ShortDatePattern").Value;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(DateTime);
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime.TryParseExact(reader.GetString(), _shortDateTimePattern, null, DateTimeStyles.None, out var result);
            return new DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second).ToString(_shortDateTimePattern));
        }
    }
}