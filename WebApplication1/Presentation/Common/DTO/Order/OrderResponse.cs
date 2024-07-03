using WebApplication1.Presentation.Common.DTO.Product;

namespace WebApplication1.Presentation.Common.DTO.Order
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public string OrderStatus { get; set; }
        public List<ProductResponseForOrder> Products { get; set; } = [];
    }
}
