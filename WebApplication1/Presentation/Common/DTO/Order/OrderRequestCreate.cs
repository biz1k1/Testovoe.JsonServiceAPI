namespace WebApplication1.Presentation.Common.DTO.Order

{
    public class OrderRequestCreate
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}
