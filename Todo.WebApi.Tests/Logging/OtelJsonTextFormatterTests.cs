using System.Diagnostics;
using System.Text.Json;
using FluentAssertions;
using Serilog.Events;
using Serilog.Parsing;
using Todo.WebApi.Configuration;
using Todo.WebApi.Logging;
using Xunit;

namespace Todo.WebApi.Tests.Logging;

public sealed class OtelJsonTextFormatterTests
{
    [Fact]
    public void Format_Should_Write_OtelCoreFields_And_Attributes()
    {
        var formatter = new OtelJsonTextFormatter(new ObservabilityResourceOptions
        {
            ServiceName = "Todo.WebApi",
            ServiceVersion = "1.2.3",
            DeploymentEnvironment = "Development"
        });

        var logEvent = CreateLogEvent(
            LogEventLevel.Information,
            "Request {Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
            null,
            ( "Method", "GET" ),
            ( "Path", "/api/todoitems" ),
            ( "StatusCode", 200 ),
            ( "ElapsedMs", 12.3 ));

        var json = Render(formatter, logEvent);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        root.GetProperty("severity_text").GetString().Should().Be("INFO");
        root.GetProperty("severity_number").GetInt32().Should().Be(9);
        root.GetProperty("trace_id").GetString().Should().Be(string.Empty);
        root.GetProperty("span_id").GetString().Should().Be(string.Empty);
        root.GetProperty("body").GetString().Should().Be("Request \"GET\" \"/api/todoitems\" responded 200 in 12.3ms");

        var resource = root.GetProperty("resource");
        resource.GetProperty("service.name").GetString().Should().Be("Todo.WebApi");
        resource.GetProperty("service.version").GetString().Should().Be("1.2.3");
        resource.GetProperty("deployment.environment").GetString().Should().Be("Development");

        var attributes = root.GetProperty("attributes");
        attributes.GetProperty("Method").GetString().Should().Be("GET");
        attributes.GetProperty("Path").GetString().Should().Be("/api/todoitems");
        attributes.GetProperty("StatusCode").GetInt32().Should().Be(200);
        attributes.GetProperty("ElapsedMs").GetDouble().Should().Be(12.3);
    }

    [Fact]
    public void Format_Should_Include_TraceAndSpanId_When_ActivityExists()
    {
        var formatter = new OtelJsonTextFormatter(new ObservabilityResourceOptions
        {
            ServiceName = "Todo.WebApi",
            ServiceVersion = "1.2.3",
            DeploymentEnvironment = "Development"
        });

        using var activity = new Activity("unit-test").Start();
        var logEvent = CreateLogEvent(LogEventLevel.Warning, "test with trace");

        var json = Render(formatter, logEvent);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        root.GetProperty("trace_id").GetString().Should().Be(activity.TraceId.ToHexString());
        root.GetProperty("span_id").GetString().Should().Be(activity.SpanId.ToHexString());
        root.GetProperty("severity_text").GetString().Should().Be("WARN");
        root.GetProperty("severity_number").GetInt32().Should().Be(13);
    }

    [Fact]
    public void Format_Should_Write_ExceptionAttributes()
    {
        var formatter = new OtelJsonTextFormatter(new ObservabilityResourceOptions
        {
            ServiceName = "Todo.WebApi",
            ServiceVersion = "1.2.3",
            DeploymentEnvironment = "Development"
        });

        var exception = CaptureException();
        var logEvent = CreateLogEvent(
            LogEventLevel.Error,
            "failed {Code}",
            exception, ( "Code", 500 )
        );

        var json = Render(formatter, logEvent);
        using var doc = JsonDocument.Parse(json);
        var attributes = doc.RootElement.GetProperty("attributes");

        attributes.GetProperty("exception.type").GetString().Should().Be(typeof(InvalidOperationException).FullName);
        attributes.GetProperty("exception.message").GetString().Should().Be("bad request");
        attributes.TryGetProperty("exception.stacktrace", out var stackTrace).Should().BeTrue();
        stackTrace.GetString().Should().NotBeNullOrWhiteSpace();
    }

    private static LogEvent CreateLogEvent(
        LogEventLevel level,
        string messageTemplate,
        Exception? exception = null,
        params (string Name, object? Value)[] properties)
    {
        var logProperties = properties
            .Select(pair => new LogEventProperty(
                        pair.Name, new ScalarValue(pair.Value)))
            .ToList();

        var parser = new MessageTemplateParser();
        return new LogEvent(
            DateTimeOffset.UtcNow,
            level,
            exception,
            parser.Parse(messageTemplate),
            logProperties);
    }

    private static string Render(OtelJsonTextFormatter formatter, LogEvent logEvent)
    {
        using var textWriter = new StringWriter();
        formatter.Format(logEvent, textWriter);
        return textWriter.ToString();
    }

    private static Exception CaptureException()
    {
        try
        {
            throw new InvalidOperationException("bad request");
        }
        catch (Exception exception)
        {
            return exception;
        }
    }
}
