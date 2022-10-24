using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    [Table("UserTable")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? ResetToken { get; set; }
        public string? Email { get; set; }
    }
}