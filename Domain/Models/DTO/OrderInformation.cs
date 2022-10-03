using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTO
{
    public class OrderInformation
    {
        public int OrderDetailId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }    
        public float Price { get; set; }
        public int CheckPaid { get; set; }
    }
}
