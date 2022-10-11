using Application.Interfaces;
using Domain.Models.EntityTest;
using Persistence.Context;

namespace Persistence.SeedingData;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly ITestRepository _repository;


    public DatabaseSeeder(ApplicationDbContext context, ITestRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public void Initialize()
    {
        AddTest();
        _context.SaveChangesAsync();
    }

    private async void AddTest()
    {
        var test = await _repository.GetTests();
        if (test.Count != 0) return;
         _context.Tests.Add(new Test() { Name = "test1" });
         _context.Tests.Add(new Test() { Name = "test2" });
    }
}