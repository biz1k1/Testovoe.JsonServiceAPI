namespace WebApplication1.Domain.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public List<Order> Orders { get; set; } = [];
    }
}
