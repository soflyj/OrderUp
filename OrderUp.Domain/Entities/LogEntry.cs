using System;

namespace OrderUp.Domain.Entities;

public class LogEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string TableName { get; set; } = null!;
    public Guid RecordId { get; set; }
    public string Action { get; set; } = null!; // e.g. "Create", "Update", "Delete"
    public string BeforeValue { get; set; } = string.Empty;
    public string AfterValue { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public Guid PerformedByUserId { get; set; }
}
