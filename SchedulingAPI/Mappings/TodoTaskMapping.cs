using SchedulingAPI.Dtos;
using SchedulingAPI.Dtos.Requests;
using SchedulingAPI.Dtos.Responses;
using SchedulingAPI.Entities;
using TaskStatus = SchedulingAPI.Entities.TaskStatus;

namespace SchedulingAPI.Mappings;

public static class TodoTaskMapping
{
    public static TodoTask ToEntity(this CreateTodoTaskDto dto)
    {
        return new TodoTask
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = TaskStatus.Pending,
            
        };
    }

    public static TodoTaskResponseDto ToDto(this TodoTask entity)
    {
        return new TodoTaskResponseDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Status = entity.Status,
        };
    }
}