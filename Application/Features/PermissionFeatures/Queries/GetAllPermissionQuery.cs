using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.PermissionFeatures.Queries;

public class GetAllPermissionQuery : IRequest<Result<IEnumerable<PermissionInfo>>>
{
}

public class GetAllPermissionQueryHandle : IRequestHandler<GetAllPermissionQuery, Result<IEnumerable<PermissionInfo>>>
{
    private readonly IMenuRepository _menuRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public GetAllPermissionQueryHandle(IPermissionRepository permissionRepository, IUserRoleRepository userRoleRepository, IMenuRepository menuRepository, IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _menuRepository = menuRepository;
        _permissionRepository = permissionRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<Result<IEnumerable<PermissionInfo>>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
    {
        var listPermission = (from p in _permissionRepository.Entities
                              join usr in _userRoleRepository.Entities on p.RoleId equals usr.RoleId
                              join m in _menuRepository.Entities on p.MenuId equals m.Id
                              join u in _userRepository.Entities on usr.UserId equals u.UserId
                              join r in _roleRepository.Entities on usr.RoleId equals r.Id
                              select new PermissionInfo()
                              {
                                  IdPermission = p.Id,
                                  MenuName = m.Name,
                                  RoleName = r.RoleName,
                                  UserName = u.Username,
                                  CanAccess = p.CanAccess,
                                  CanAdd = p.CanAdd,
                                  CanDelete = p.CanDelete,
                                  CanEdit = p.CanEdit
                              });

        return await Result<IEnumerable<PermissionInfo>>.SuccessAsync(listPermission);
    }
}