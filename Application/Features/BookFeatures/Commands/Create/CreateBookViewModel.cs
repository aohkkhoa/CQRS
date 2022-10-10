using System.ComponentModel;

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