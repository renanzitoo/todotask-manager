using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SchedulingAPI.Context;
using SchedulingAPI.Dtos;
using SchedulingAPI.Dtos.Requests;
using SchedulingAPI.Entities;
using SchedulingAPI.Services;
using TaskStatus = SchedulingAPI.Entities.TaskStatus;

namespace SchedulingAPI.Tests.Unit.Services;

public class TodoTaskControllerTests
{
    private TodoTaskContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TodoTaskContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TodoTaskContext(options);
    }

    private TodoTaskService CreateService(TodoTaskContext context)
    {
        return new TodoTaskService(context);
    }

    // -------------------------
    // CREATE
    // -------------------------

    [Fact]
    public async Task CreateAsync_ShouldCreateTaskSuccessfully()
    {
        // Arrange
        var context = CreateContext();
        var service = CreateService(context);

        var dto = new CreateTodoTaskDto
        {
            Title = "Test Task",
            Description = "Test Description"
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

    // -------------------------
    // GET ALL
    // -------------------------

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTasks()
    {
        // Arrange
        var context = CreateContext();
        context.TodoTasks.AddRange(
            new TodoTask
            {
                Title = "Task 1",
                Description = "Desc 1",
                Status = TaskStatus.Pending
            },
            new TodoTask
            {
                Title = "Task 2",
                Description = "Desc 2",
                Status = TaskStatus.Completed
            }
        );
        await context.SaveChangesAsync();

        var service = CreateService(context);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Select(t => t.Title).Should().Contain(new[] { "Task 1", "Task 2" });
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoTasksExist()
    {
        // Arrange
        var context = CreateContext();
        var service = CreateService(context);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    // -------------------------
    // GET BY ID
    // -------------------------

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTask_WhenTaskExists()
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

        var service = CreateService(context);

        // Act
        var result = await service.GetByIdAsync(task.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(task.Id);
        result.Title.Should().Be(task.Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenTaskDoesNotExist()
    {
        // Arrange
        var context = CreateContext();
        var service = CreateService(context);

        // Act
        var result = await service.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    // -------------------------
    // DELETE
    // -------------------------

    [Fact]
    public async Task DeleteAsync_ShouldDeleteTask_WhenTaskExists()
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

        var service = CreateService(context);

        // Act
        var result = await service.DeleteAsync(task.Id);

        // Assert
        result.Should().BeTrue();
        context.TodoTasks.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenTaskDoesNotExist()
    {
        // Arrange
        var context = CreateContext();
        var service = CreateService(context);

        // Act
        var result = await service.DeleteAsync(1);

        // Assert
        result.Should().BeFalse();
    }
}
