using WebApplication1.Domain.Enums;

namespace WebApplication1.Presentation.Common.DTO_s
{
    public class OrderRequestCreate
    {
        public Guid ProductId { get; set; }
        public int AmountProduct { get; set; }
    }
}
