namespace WebApplication1.Application.Common.Exceptions
{
    public class ExhaustiveLimitOfProducts:Exception
    {
        public override string Message => "Вы пытаетесь заказать больше товаров, чем есть на складке";
    }
}
