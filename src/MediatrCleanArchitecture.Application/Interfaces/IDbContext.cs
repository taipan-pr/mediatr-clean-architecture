using MediatrCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatrCleanArchitecture.Application.Interfaces;

public interface IDbContext
{
    DbSet<Employee> Employees { get; set; }
    Task SeedDatabase();
    Task<int> SaveChangesAsync();
}
