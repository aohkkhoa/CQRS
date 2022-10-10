using Application.Interfaces;
using Domain.Models.EntityTest;
using Persistence.Context;

namespace Persistence.Repositories;

public class TestRepository : ITestRepository
{
    
    private readonly ApplicationDbContext _context;

    public TestRepository(ApplicationDbContext context)
    {
        _context=context;
    }
    public Task<List<Test>> GetTests()
    {
        var tests = (from a in _context.Tests
            select a).ToList();
        return Task.FromResult(tests);
    }
}