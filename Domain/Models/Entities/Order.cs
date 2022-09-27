using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
