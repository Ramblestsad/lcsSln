# TODO

## 微服务基础能力

- [x] 为四个运行单元增加独立 Dockerfile 和 Kubernetes Deployment。
- [ ] 为所有服务补充分布式 Trace，串联 HTTP、RabbitMQ 和数据库调用。
- [ ] 为所有 Deployment 增加 liveness probe 和 readiness probe。
- [ ] 为服务调用和消息消费增加超时、有限重试和死信队列。
- [ ] 增加跨服务集成测试和端到端测试，覆盖完整订单流程。
- [ ] 使用 Kubernetes migration Job 执行数据库迁移，移除服务启动迁移。
- [ ] 在 CI 中构建和发布独立服务镜像，并增加自动部署流程。
- [ ] 增加基于 YARP 的 API Gateway 示例。

## 业务示例

- [x] 使用 Serilog 输出符合 OpenTelemetry 语义的 JSON 日志，开发环境保留可读日志。
- [x] 接入 OpenTelemetry Collector，导出日志、指标和 Trace。
- [x] 增加 Redis 示例接口。
- [x] 增加 SignalR WebSocket 示例接口。
- [x] 增加 RabbitMQ 订单流程，使用 Inbox 和 Outbox 保证消息处理可靠性。
- [x] 增加 PostgreSQL 主从读写分离示例。
- [x] 将主 API、订单 API、订单 Worker 和库存 Worker 拆成独立运行单元。
- [x] 使用单个 PostgreSQL 数据库和三个独立 EF Core 迁移历史表。
- [ ] 增加订单超时取消，比较被动轮询、延迟队列和时间轮方案。
- [ ] 增加文件上传和下载，支持大文件分片上传与断点续传。
- [ ] 增加视频处理服务示例。
- [ ] 增加数据库集群应用示例，覆盖数据路由和事务约束。
