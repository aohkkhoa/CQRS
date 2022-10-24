using Application.Interfaces;
using Domain.Models.Entities;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}