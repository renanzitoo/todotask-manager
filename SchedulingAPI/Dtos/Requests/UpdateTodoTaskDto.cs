using System.ComponentModel.DataAnnotations;

namespace SchedulingAPI.Dtos.Requests;

public class UpdateTodoTaskDto
{
    [Required]
    [MaxLength(30)]
    public string Title { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public TaskStatus Status { get; set; }
}