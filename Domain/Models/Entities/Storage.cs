using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
