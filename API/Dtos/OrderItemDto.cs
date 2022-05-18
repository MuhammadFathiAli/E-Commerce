namespace API.Dtos
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}