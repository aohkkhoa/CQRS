namespace Domain.Models.DTO
{
    public class OrderCreate
    {
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
    }
}
