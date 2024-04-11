using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Todo.DAL.Entity;

public class TodoItem
{
    [Key] public long Id { get; set; }

    [Required] public string? Name { get; set; }

    [DefaultValue(false)] public bool IsComplete { get; set; }
}
