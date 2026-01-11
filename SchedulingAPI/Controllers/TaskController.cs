using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulingAPI.Context;
using SchedulingAPI.Entities;
using TaskStatus = SchedulingAPI.Entities.TaskStatus;


namespace SchedulingAPI.Controllers;

[ApiController]
[Route("api/tasks")]

public class TodoTaskController : ControllerBase
{
    private readonly TodoTaskContext _context;

    public TodoTaskController(TodoTaskContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoTask task)
    {
        task.DueDate = DateTime.Now;
        await _context.TodoTasks.AddAsync(task);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction("Create", new { id = task.Id }, task);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _context.TodoTasks.ToListAsync();
        return Ok(tasks);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _context.TodoTasks.FindAsync(id);
        if (task == null)
            return NotFound();
        
        return Ok(task);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TodoTask task)
    {
        if (id != task.Id)
        {
            return BadRequest("Route id does not match task id");
        }
        
        var existingTask = await _context.TodoTasks.FindAsync(id);
        if (existingTask == null)
            return NotFound();
        
        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.DueDate = task.DueDate;
        existingTask.Status = task.Status;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.TodoTasks.FindAsync(id);
        if (task == null)
            return NotFound();
        
        _context.TodoTasks.Remove(task);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDueDate([FromQuery] DateTime? dueDate)
    {
        var query = _context.TodoTasks.AsQueryable();

        if (dueDate.HasValue)
            query = query.Where(t => t.DueDate.Date == dueDate.Value.Date);

        return Ok(await query.ToListAsync());
    }
    
    [HttpGet("by-status")]
    public async Task<IActionResult> GetByStatus([FromQuery] TaskStatus status)
    {
        return Ok(await _context.TodoTasks
            .Where(t => t.Status == status)
            .ToListAsync());
    }
    
    [HttpGet("by-title")]
    public async Task<IActionResult> GetByTitle([FromQuery] String title)
    {
        return Ok(await _context.TodoTasks
            .Where(t => t.Title == title)
            .ToListAsync());
    }
}