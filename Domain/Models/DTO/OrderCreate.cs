using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTO
{
    public class OrderCreate
    {
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
