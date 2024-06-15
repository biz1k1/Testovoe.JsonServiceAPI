namespace WebApplication1.Presentation.Common.DTO

{
    public class OrderRequestCreate
    {
        public Guid ProductId { get; set; }
        public int AmountProduct { get; set; }
    }
}
