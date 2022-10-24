using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    [Table("Storage")]
    public class Storage
    {
        [Key]
        public int StorageId { get; set; }

        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}