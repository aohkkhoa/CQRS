using Application.Exceptions;
using Application.Interfaces;
using Domain.Models.Entities;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.PermissionFeatures.Commands
{
    public class EditPermissionCommand : IRequest<Result<Permission>>
    {
        public int PermissionId { get; set; }

        public bool CanAccess { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class EditPermissionCommandHandle : IRequestHandler<EditPermissionCommand, Result<Permission>>
    {
        public readonly IPermissionRepository _permissionRepository;

        public EditPermissionCommandHandle(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<Permission>> Handle(EditPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = _permissionRepository.Entities.FirstOrDefault(p => p.Id == request.PermissionId);
            if (permission is null)
                throw new ApiException("Permission Not Found !");

            permission.CanAccess = request.CanAccess;
            permission.CanEdit = request.CanEdit;
            permission.CanDelete = request.CanDelete;
            permission.CanAdd = request.CanAdd;
            await _permissionRepository.Save();
            return await Result<Permission>.SuccessAsync("Edit Complete !");
        }
    }
}