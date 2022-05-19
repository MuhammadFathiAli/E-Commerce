using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.1,double.MaxValue, ErrorMessage ="Price must be grater than zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,double.MaxValue, ErrorMessage = "Quantity must be grater than zero")]
        public int Quantity { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
