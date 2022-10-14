using Application.Interfaces;
using Domain.Models.DTO;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.PermissionFeatures.Queries;

public class GetAllPermissionQuery : IRequest<Result<List<PermissionInfo>>>
{
}

public class GetAllPermissionQueryHandle : IRequestHandler<GetAllPermissionQuery, Result<List<PermissionInfo>>>
{
    private readonly IPermissionRepository _permissionRepository;

    public GetAllPermissionQueryHandle(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<Result<List<PermissionInfo>>> Handle(GetAllPermissionQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _permissionRepository.GetAllPermission();
        return await Result<List<PermissionInfo>>.SuccessAsync(result);
    }
}