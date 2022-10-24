using Application.Interfaces;
using Domain.Models.Entities;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.Repository;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}