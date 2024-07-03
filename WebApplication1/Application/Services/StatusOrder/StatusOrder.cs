using WebApplication1.Domain.Enums;

namespace WebApplication1.Application.Services.StatusOrder
{
    public class StatusOrder : IStatusOrder
    {
        public void CheckStatusOrder(string status)
        {
            bool isDefined = Enum.IsDefined(typeof(OrderStatus), status);

            if (isDefined == false)
            {
                throw new Exception();
            }
        }
    }
}
