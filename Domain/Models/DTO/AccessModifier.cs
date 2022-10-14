namespace Domain.Models.DTO;

public class AccessModifier
{
    public bool CanAdd { get; set; }
    public bool CanEdit { get; set; }
    public bool CanAccess { get; set; }
    public bool CanDelete { get; set; }
}