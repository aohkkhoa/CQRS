using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    [Table("OrderMain")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int BookId { get; set; }
        public int CustomerId { get; set; }
    }
}