using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    [Table("UserTable")]
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string userName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string? ResetToken { get; set; }
    }
}
