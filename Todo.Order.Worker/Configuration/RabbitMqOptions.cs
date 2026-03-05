namespace Todo.Order.Worker.Configuration;

public sealed class RabbitMqOptions
{
    public const string SectionName = "RabbitMq";

    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string Username { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
    public string OrderExchange { get; set; } = "todo.order.exchange";
    public string InventoryExchange { get; set; } = "todo.inventory.exchange";
    public string InventoryReserveQueue { get; set; } = "todo.inventory.reserve.q";
    public string OrderResultQueue { get; set; } = "todo.order.result.q";
    public int RetryCount { get; set; } = 5;
}
