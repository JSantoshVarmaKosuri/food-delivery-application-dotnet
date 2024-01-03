using System.ComponentModel.DataAnnotations;

namespace fdorder.DataAccess.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid RestarentId { get; set; }

        public Guid? PaymentId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public ICollection<OrderItem> OrderItems { get; set; } = [];

        [Required]
        public byte Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}