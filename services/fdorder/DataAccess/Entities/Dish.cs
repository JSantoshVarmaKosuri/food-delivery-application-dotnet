using System.ComponentModel.DataAnnotations;

namespace fdorder.DataAccess.Entities
{
    public class Dish
    {
        [Key]
        public Guid DishId { get; set; }

        [Required]
        public Guid OrderItemId { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public Int16 Quantity { get; set; }

        [Required]
        public decimal Price { get; set; } = 0.00m;
    }
}