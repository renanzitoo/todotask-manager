using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SchedulingAPI.Context;
using SchedulingAPI.Dtos.Requests;
using SchedulingAPI.Entities;
using SchedulingAPI.Services;
using TaskStatus = SchedulingAPI.Entities.TaskStatus;

namespace SchedulingAPI.Tests.Unit.Services;

public class TodoTaskServiceTests
{
    private TodoTaskContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TodoTaskContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TodoTaskContext(options);
    }
    [Fact]
    public async Task CreateAsync_ShouldCreateTaskSuccessfully()
    {
        // Arrange
        var context = CreateContext();
        var service = new TodoTaskService(context);

        var dto = new CreateTodoTaskDto
        {
            Title = "Test Task",
            Description = "Test Description",
            Status = 0
        };

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Title.Should().Be(dto.Title);
        result.Description.Should().Be(dto.Description);
        result.Status.Should().Be(TaskStatus.Pending);

        context.TodoTasks.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnTasks()
    {
        // Arrange
        var context = CreateContext();
        context.TodoTasks.Add(new TodoTask
        {
            Title = "Task 1",
            Description = "Desc 1",
            Status = TaskStatus.Pending
        });
        await context.SaveChangesAsync();

        var service = new TodoTaskService(context);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Should().HaveCount(1);
        result.First().Title.Should().Be("Task 1");
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnTask_WhenExists()
    {
        // Arrange
        var context = CreateContext();
        var task = new TodoTask
        {
            Title = "Task",
            Description = "Desc",
            Status = TaskStatus.Pending
        };
        context.TodoTasks.Add(task);
        await context.SaveChangesAsync();

        var service = new TodoTaskService(context);

        // Act
        var result = await service.GetByIdAsync(task.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(task.Id);
    }
    [Fact]
    public async Task DeleteAsync_ShouldRemoveTask()
    {
        // Arrange
        var context = CreateContext();
        var task = new TodoTask
        {
            Title = "Task",
            Description = "Desc",
            Status = TaskStatus.Pending
        };
        context.TodoTasks.Add(task);
        await context.SaveChangesAsync();

        var service = new TodoTaskService(context);

        // Act
        var result = await service.DeleteAsync(task.Id);

        // Assert
        result.Should().BeTrue();
        context.TodoTasks.Should().BeEmpty();
    }
    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenNotFound()
    {
        // Arrange
        var context = CreateContext();
        var service = new TodoTaskService(context);

        // Act
        var result = await service.DeleteAsync(1);

        // Assert
        result.Should().BeFalse();
    }
    
}