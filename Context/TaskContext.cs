using Microsoft.EntityFrameworkCore;
using SchedulingAPI.Entities;

namespace SchedulingAPI.Context;

public class TodoTaskContext : DbContext
{
    public TodoTaskContext(DbContextOptions<TodoTaskContext> options) :  base(options) 
    {
        
    }
    
    public DbSet<TodoTask> TodoTasks { get; set; }
}