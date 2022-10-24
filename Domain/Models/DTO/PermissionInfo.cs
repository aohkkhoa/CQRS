namespace Domain.Models.DTO;

public class PermissionInfo
{
    public int IdPermission { get; set; }
    public string UserName { get; set; }
    public string RoleName { get; set; }
    public string MenuName { get; set; }
    public bool CanAdd { get; set; }
    public bool CanEdit { get; set; }
    public bool CanAccess { get; set; }
    public bool CanDelete { get; set; }
    public bool IsDeleted { get; set; }
    
}