using Core.Entities;

using System.ComponentModel.DataAnnotations;

namespace SkinetAppAPI.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
