# OpenTelemetry Collector Example

This folder contains an example collector config for a `Loki + Tempo + Prometheus` stack.

The file is a reference only:

- The collector is assumed to be deployed and managed outside this repo.
- Adapt endpoints, log include paths, and authentication headers for your environment.

## Application-side environment variables

Set these on `Todo.WebApi` (or equivalent deployment target):

```bash
OTEL_SERVICE_NAME=todo-webapi
OTEL_EXPORTER_OTLP_ENDPOINT=http://otel-collector:4317
OTEL_EXPORTER_OTLP_PROTOCOL=grpc
OTEL_TRACES_SAMPLER=parentbased_traceidratio
OTEL_TRACES_SAMPLER_ARG=1.0
```

Optional:

```bash
OTEL_EXPORTER_OTLP_HEADERS=Authorization=Bearer <token>
```

## Local validation steps

1. Start collector/backends in Docker (`collector + loki + tempo + prometheus`).
2. Start API:

```bash
dotnet run --project Todo.WebApi.csproj --launch-profile Local
```

3. Generate traffic:

```bash
curl -i http://localhost:5173/api/todoitems
```

4. Check logs in Loki and copy one `trace_id`.
5. Query the same `trace_id` in Tempo.
6. Check metrics ingestion in Prometheus.

## Notes

- Logs are emitted by the app to `stdout` and collected via `filelog`.
- Traces and metrics are exported directly from the app via OTLP.
- Development logging remains human-readable; non-development logging remains OTel JSON.
