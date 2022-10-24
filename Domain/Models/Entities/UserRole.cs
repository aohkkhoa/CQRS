using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    [Table("UserRole")]
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}