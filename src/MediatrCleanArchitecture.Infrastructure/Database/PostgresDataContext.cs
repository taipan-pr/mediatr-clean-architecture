using MediatrCleanArchitecture.Application.Interfaces;
using MediatrCleanArchitecture.Domain.Entities;
using MediatrCleanArchitecture.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MediatrCleanArchitecture.Infrastructure.Database;

internal class PostgresDataContext : DbContext, IDbContext
{
    public const string ConnectionStringName = "Postgres";

    public PostgresDataContext(DbContextOptions options) : base(options) { }

    public required DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }

    public async Task SeedDatabase()
    {
        // Run migration scripts
        await Database.MigrateAsync();

        // Clear data
        Employees?.RemoveRange(Employees);

        // Insert data
        Employees?.AddRangeAsync(
            new Employee
            {
                Id = "406d7787a20b4b41a1ff8d4b26a32f40",
                FirstName = "Taipan",
                LastName = "Prasithpongchai"
            },
            new Employee
            {
                Id = Guid.NewGuid().ToString("N"),
                FirstName = "Foo",
                LastName = "Bar"
            }
        );

        await SaveChangesAsync();
    }
}
