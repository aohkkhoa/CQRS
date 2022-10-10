using Domain.Models.EntityTest;

namespace Application.Interfaces;

public interface ITestRepository
{
    Task<List<Test>> GetTests();
}