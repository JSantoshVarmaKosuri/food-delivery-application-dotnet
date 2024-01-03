using System.ComponentModel.DataAnnotations;

namespace fdorder.DataAccess.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public ICollection<Dish> Dishes { get; set; } = [];
    }
}