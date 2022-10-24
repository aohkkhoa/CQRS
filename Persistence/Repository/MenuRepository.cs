using Application.Interfaces;
using Domain.Models.Entities;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.Repository;

public class MenuRepository : GenericRepository<Menu>, IMenuRepository
{
    public MenuRepository(ApplicationDbContext context) : base(context)
    {
    }
}