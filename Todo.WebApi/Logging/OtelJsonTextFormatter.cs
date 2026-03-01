using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Serilog.Events;
using Serilog.Formatting;
using Todo.WebApi.Configuration;

namespace Todo.WebApi.Logging;

public sealed class OtelJsonTextFormatter: ITextFormatter
{
    private readonly string serviceName;
    private readonly string serviceVersion;
    private readonly string deploymentEnvironment;

    public OtelJsonTextFormatter(ObservabilityResourceOptions options)
    {
        serviceName = string.IsNullOrWhiteSpace(options.ServiceName)
            ? "Todo.WebApi"
            : options.ServiceName;
        serviceVersion = string.IsNullOrWhiteSpace(options.ServiceVersion)
            ? "unknown"
            : options.ServiceVersion;
        deploymentEnvironment = string.IsNullOrWhiteSpace(options.DeploymentEnvironment)
            ? "Production"
            : options.DeploymentEnvironment;
    }

    public void Format(LogEvent logEvent, TextWriter output)
    {
        ArgumentNullException.ThrowIfNull(logEvent);
        ArgumentNullException.ThrowIfNull(output);

        var severity = GetSeverity(logEvent.Level);
        var traceId = ResolveTraceId(logEvent);
        var spanId = ResolveSpanId(logEvent);

        using var memoryStream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(memoryStream))
        {
            writer.WriteStartObject();
            writer.WriteString("time_unix_nano", ToUnixTimeNanoseconds(logEvent.Timestamp));
            writer.WriteString("observed_time_unix_nano", ToUnixTimeNanoseconds(DateTimeOffset.UtcNow));
            writer.WriteNumber("severity_number", severity.Number);
            writer.WriteString("severity_text", severity.Text);
            writer.WriteString("body", logEvent.RenderMessage(CultureInfo.InvariantCulture));
            writer.WriteString("trace_id", traceId);
            writer.WriteString("span_id", spanId);

            writer.WriteStartObject("resource");
            writer.WriteString("service.name", serviceName);
            writer.WriteString("service.version", serviceVersion);
            writer.WriteString("deployment.environment", deploymentEnvironment);
            writer.WriteEndObject();

            writer.WriteStartObject("attributes");
            writer.WriteString("message_template", logEvent.MessageTemplate.Text);

            foreach (var property in logEvent.Properties)
            {
                if (property.Key is ActivityContextEnricher.TraceIdPropertyName
                    or ActivityContextEnricher.SpanIdPropertyName)
                {
                    continue;
                }

                writer.WritePropertyName(property.Key);
                WriteLogEventPropertyValue(writer, property.Value);
            }

            if (logEvent.Exception is not null)
            {
                writer.WriteString("exception.type", logEvent.Exception.GetType().FullName);
                writer.WriteString("exception.message", logEvent.Exception.Message);
                writer.WriteString("exception.stacktrace", logEvent.Exception.ToString());
            }

            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        output.Write(Encoding.UTF8.GetString(memoryStream.ToArray()));
        output.WriteLine();
    }

    private static (string Text, int Number) GetSeverity(LogEventLevel level)
    {
        return level switch
        {
            LogEventLevel.Verbose => ( "TRACE", 1 ),
            LogEventLevel.Debug => ( "DEBUG", 5 ),
            LogEventLevel.Information => ( "INFO", 9 ),
            LogEventLevel.Warning => ( "WARN", 13 ),
            LogEventLevel.Error => ( "ERROR", 17 ),
            LogEventLevel.Fatal => ( "FATAL", 21 ),
            _ => ( "INFO", 9 )
        };
    }

    private static string ResolveTraceId(LogEvent logEvent)
    {
        var propertyValue = ExtractStringProperty(
            logEvent, ActivityContextEnricher.TraceIdPropertyName);
        if (!string.IsNullOrWhiteSpace(propertyValue))
        {
            return propertyValue;
        }

        return Activity.Current?.TraceId.ToHexString() ?? string.Empty;
    }

    private static string ResolveSpanId(LogEvent logEvent)
    {
        var propertyValue = ExtractStringProperty(
            logEvent, ActivityContextEnricher.SpanIdPropertyName);
        if (!string.IsNullOrWhiteSpace(propertyValue))
        {
            return propertyValue;
        }

        return Activity.Current?.SpanId.ToHexString() ?? string.Empty;
    }

    private static string? ExtractStringProperty(LogEvent logEvent, string propertyName)
    {
        if (!logEvent.Properties.TryGetValue(propertyName, out var propertyValue))
        {
            return null;
        }

        if (propertyValue is ScalarValue { Value: string stringValue })
        {
            return stringValue;
        }

        return propertyValue.ToString().Trim('"');
    }

    private static string ToUnixTimeNanoseconds(DateTimeOffset value)
    {
        var ticksSinceUnixEpoch = value.UtcDateTime.Ticks - DateTime.UnixEpoch.Ticks;
        var nanoseconds = ticksSinceUnixEpoch * 100;
        return nanoseconds.ToString(CultureInfo.InvariantCulture);
    }

    private static void WriteLogEventPropertyValue(Utf8JsonWriter writer, LogEventPropertyValue value)
    {
        switch (value)
        {
            case ScalarValue scalarValue:
                WriteScalarValue(writer, scalarValue.Value);
                return;
            case SequenceValue sequenceValue:
                writer.WriteStartArray();
                foreach (var item in sequenceValue.Elements)
                {
                    WriteLogEventPropertyValue(writer, item);
                }

                writer.WriteEndArray();
                return;
            case StructureValue structureValue:
                writer.WriteStartObject();
                foreach (var property in structureValue.Properties)
                {
                    writer.WritePropertyName(property.Name);
                    WriteLogEventPropertyValue(writer, property.Value);
                }

                writer.WriteEndObject();
                return;
            case DictionaryValue dictionaryValue:
                writer.WriteStartObject();
                foreach (var entry in dictionaryValue.Elements)
                {
                    var key = entry.Key.Value?.ToString() ?? string.Empty;
                    writer.WritePropertyName(key);
                    WriteLogEventPropertyValue(writer, entry.Value);
                }

                writer.WriteEndObject();
                return;
            default:
                writer.WriteStringValue(value.ToString());
                return;
        }
    }

    private static void WriteScalarValue(Utf8JsonWriter writer, object? value)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        switch (value)
        {
            case string stringValue:
                writer.WriteStringValue(stringValue);
                return;
            case bool boolValue:
                writer.WriteBooleanValue(boolValue);
                return;
            case byte byteValue:
                writer.WriteNumberValue(byteValue);
                return;
            case sbyte sbyteValue:
                writer.WriteNumberValue(sbyteValue);
                return;
            case short shortValue:
                writer.WriteNumberValue(shortValue);
                return;
            case ushort ushortValue:
                writer.WriteNumberValue(ushortValue);
                return;
            case int intValue:
                writer.WriteNumberValue(intValue);
                return;
            case uint uintValue:
                writer.WriteNumberValue(uintValue);
                return;
            case long longValue:
                writer.WriteNumberValue(longValue);
                return;
            case ulong ulongValue:
                writer.WriteNumberValue(ulongValue);
                return;
            case float floatValue:
                writer.WriteNumberValue(floatValue);
                return;
            case double doubleValue:
                writer.WriteNumberValue(doubleValue);
                return;
            case decimal decimalValue:
                writer.WriteNumberValue(decimalValue);
                return;
            case DateTimeOffset dateTimeOffsetValue:
                writer.WriteStringValue(dateTimeOffsetValue);
                return;
            case DateTime dateTimeValue:
                writer.WriteStringValue(dateTimeValue);
                return;
            case Guid guidValue:
                writer.WriteStringValue(guidValue);
                return;
            case TimeSpan timeSpanValue:
                writer.WriteStringValue(timeSpanValue.ToString("c", CultureInfo.InvariantCulture));
                return;
            default:
                writer.WriteStringValue(value.ToString());
                return;
        }
    }
}
