using System.ComponentModel.DataAnnotations;

namespace Todo.DAL.Entity;

public class OrderOutboxMessage
{
    [Key]
    public long Id { get; set; }

    public Guid MessageId { get; set; }

    public Guid CorrelationId { get; set; }

    [Required]
    [MaxLength(128)]
    public string EventType { get; set; } = null!;

    [Required]
    public string Payload { get; set; } = null!;

    public DateTimeOffset OccurredOnUtc { get; set; }

    public DateTimeOffset? PublishedOnUtc { get; set; }

    public int RetryCount { get; set; }

    [MaxLength(1024)]
    public string? LastError { get; set; }
}

public class OrderInboxMessage
{
    [Key]
    public long Id { get; set; }

    public Guid MessageId { get; set; }

    [Required]
    [MaxLength(128)]
    public string Consumer { get; set; } = null!;

    public DateTimeOffset ProcessedAtUtc { get; set; }
}

public class InventoryOutboxMessage
{
    [Key]
    public long Id { get; set; }

    public Guid MessageId { get; set; }

    public Guid CorrelationId { get; set; }

    [Required]
    [MaxLength(128)]
    public string EventType { get; set; } = null!;

    [Required]
    public string Payload { get; set; } = null!;

    public DateTimeOffset OccurredOnUtc { get; set; }

    public DateTimeOffset? PublishedOnUtc { get; set; }

    public int RetryCount { get; set; }

    [MaxLength(1024)]
    public string? LastError { get; set; }
}

public class InventoryInboxMessage
{
    [Key]
    public long Id { get; set; }

    public Guid MessageId { get; set; }

    [Required]
    [MaxLength(128)]
    public string Consumer { get; set; } = null!;

    public DateTimeOffset ProcessedAtUtc { get; set; }
}
