using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTO
{
    public class StorageUnit
    {
        public int StorageUnitId { get; set; }
        public string BookName { get; set; }
        public int Quantity { get; set; }
    }
}
