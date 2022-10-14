using Domain.Models.DTO;

namespace Application.Interfaces;

public interface IPermissionRepository
{
    /// <summary>
    /// lay ds permisson
    /// </summary>
    /// <returns></returns>
    Task<List<PermissionInfo>> GetAllPermission();
}