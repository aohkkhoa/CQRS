using Application.Interfaces;
using Domain.Models.DTO;
using Persistence.Context;

namespace Persistence.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _context;

    public PermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<PermissionInfo>> GetAllPermission()
    {
        var permissions = (from p in _context.Permissions
            join usr in _context.UserRoles on p.RoleId equals usr.RoleId
            join m in _context.Menus on p.MenuId equals m.Id
            join u in _context.Users on usr.UserId equals u.userId
            join r in _context.Roles on usr.RoleId equals r.Id
            select new PermissionInfo()
            {
                IdPermission = p.Id,
                MenuName = m.Name,
                RoleName = r.RoleName,
                UserName = u.userName
            }).ToList();
        return Task.FromResult(permissions);
    }
}