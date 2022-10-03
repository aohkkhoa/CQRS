using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTO
{
    public class BookInformation
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
    }
}
