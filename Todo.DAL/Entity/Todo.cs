using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Todo.DAL.Entity;

public class TodoItem {
    [Key] public long Id { get; set; }

    [Required] [MaxLength(512)] public string Name { get; set; } = null!;

    [Length(8, 256)] [MaxLength(256)] public string? Description { get; set; }

    [DefaultValue(false)] public bool Completed { get; set; }
}
