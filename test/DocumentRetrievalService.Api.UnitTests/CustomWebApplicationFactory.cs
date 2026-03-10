using DocumentRetrievalService.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentRetrievalService.Api.UnitTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DocumentDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Create and open a new SQLite in-memory connection
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<DocumentDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });
        });

        builder.UseEnvironment("Testing");
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection?.Close();
        _connection?.Dispose();
    }
}
