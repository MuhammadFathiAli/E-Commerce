using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomeBaksetDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
