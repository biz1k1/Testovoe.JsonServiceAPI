using WebApplication1.Presentation.Common.DTO_s.Product;

namespace WebApplication1.Presentation.Common.DTO_s.Order
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public string OrderStatus { get; set; }
        public List<ProductResponse> Products { get; set; } = [];
    }
}
