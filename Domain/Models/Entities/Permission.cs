using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    [Table("Permission")]
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanAccess { get; set; }
        public bool CanDelete { get; set; }
        public bool IsDeleted { get; set; }
    }
}
