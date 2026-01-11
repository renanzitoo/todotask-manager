using SchedulingAPI.Dtos;
using SchedulingAPI.Dtos.Requests;
using SchedulingAPI.Dtos.Responses;

namespace SchedulingAPI.Services.Interfaces;

public interface ITodoTaskService
{
    Task<TodoTaskResponseDto> CreateAsync(CreateTodoTaskDto dto);
    Task<IEnumerable<TodoTaskResponseDto>> GetAllAsync();
    Task<TodoTaskResponseDto?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
}