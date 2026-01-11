using Microsoft.AspNetCore.Mvc;
using SchedulingAPI.Dtos;
using SchedulingAPI.Dtos.Requests;
using SchedulingAPI.Services.Interfaces;

namespace SchedulingAPI.Controllers;

[ApiController]
[Route("api/tasks")]
public class TodoTaskController : ControllerBase
{
    private readonly ITodoTaskService _service;

    public TodoTaskController(ITodoTaskService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoTaskDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}