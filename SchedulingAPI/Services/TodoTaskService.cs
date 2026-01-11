using Microsoft.EntityFrameworkCore;
using SchedulingAPI.Context;
using SchedulingAPI.Dtos;
using SchedulingAPI.Dtos.Requests;
using SchedulingAPI.Dtos.Responses;
using SchedulingAPI.Entities;
using SchedulingAPI.Mappings;
using SchedulingAPI.Services.Interfaces;

namespace SchedulingAPI.Services;

public class TodoTaskService : ITodoTaskService
{
    private readonly TodoTaskContext _context;

    public TodoTaskService(TodoTaskContext context)
    {
        _context = context;
    }

    public async Task<TodoTaskResponseDto> CreateAsync(CreateTodoTaskDto dto)
    {
        var entity = dto.ToEntity();

        _context.TodoTasks.Add(entity);
        await _context.SaveChangesAsync();

        return entity.ToDto();
    }

    public async Task<IEnumerable<TodoTaskResponseDto>> GetAllAsync()
    {
        return await _context.TodoTasks
            .AsNoTracking()
            .Select(t => t.ToDto())
            .ToListAsync();
    }

    public async Task<TodoTaskResponseDto?> GetByIdAsync(int id)
    {
        var entity = await _context.TodoTasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => Equals(t.Id, id));

        return entity?.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.TodoTasks.FindAsync(id);

        if (entity is null)
            return false;

        _context.TodoTasks.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }
}