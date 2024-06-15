using WebApplication1.Domain.Enums;

namespace WebApplication1.Domain.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; } 
        public List<Product> Products { get; set; } = [];
    }
}
