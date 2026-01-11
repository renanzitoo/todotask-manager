using System.ComponentModel.DataAnnotations;

namespace SchedulingAPI.Entities;

public class TodoTask
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Title { get; set; }

    [Required]
    [MaxLength(200)]
    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    public TaskStatus Status { get; set; }
}

public enum TaskStatus
{
    Pending,
    InProgress,
    Completed,
    Cancelled
}