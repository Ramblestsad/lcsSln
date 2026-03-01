using System.Diagnostics;
using Serilog.Core;
using Serilog.Events;

namespace Todo.WebApi.Logging;

public sealed class ActivityContextEnricher: ILogEventEnricher
{
    internal const string TraceIdPropertyName = "trace_id";
    internal const string SpanIdPropertyName = "span_id";

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var activity = Activity.Current;
        if (activity is null)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(TraceIdPropertyName, string.Empty));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(SpanIdPropertyName, string.Empty));
            return;
        }

        logEvent.AddPropertyIfAbsent(
            propertyFactory.CreateProperty(TraceIdPropertyName, activity.TraceId.ToHexString()));
        logEvent.AddPropertyIfAbsent(
            propertyFactory.CreateProperty(SpanIdPropertyName, activity.SpanId.ToHexString()));
    }
}
