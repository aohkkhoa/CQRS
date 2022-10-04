using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BookFeatures.Commands.Create
{
    public class CreateBookViewModel
    {
        public string Title { get; set; }
        [DisplayName("Author")]
        public string Author { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}
