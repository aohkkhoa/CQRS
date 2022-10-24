using Application.Features.PermissionFeatures.Commands;
using Application.Features.PermissionFeatures.Queries;
using Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Wrapper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController
    {
        public PermissionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<Result> GetAll()
        {
            return await _mediator.Send(new GetAllPermissionQuery());
        }

        [HttpPut]
        public async Task<Result<Permission>> EditPermission(int PermissionId, bool CanAccess, bool CanAdd, bool CanEdit, bool CanDelete)
        {
            return await _mediator.Send(new EditPermissionCommand() { CanAccess = CanAccess, CanAdd = CanAdd, CanDelete = CanDelete, CanEdit = CanEdit, PermissionId = PermissionId });
        }
    }
}