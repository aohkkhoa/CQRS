using Application.Interfaces;
using Domain.Models.DTO;
using Domain.Models.Entities;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.Repository;

public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext context) : base(context)
    {
    }
}