using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchedulingAPI.Context;

namespace SchedulingAPI.Tests.Integration;
//This class sets up an in-memory database for integration testing, to not save any data on database.
public class ApiFactory :  WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<TodoTaskContext>)
            );

            services.Remove(descriptor);

            services.AddDbContext<TodoTaskContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });

        });
    }
    
}