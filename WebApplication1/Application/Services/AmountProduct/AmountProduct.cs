using WebApplication1.Application.Common.Exceptions;
using WebApplication1.Domain.Entity;
using WebApplication1.Infrastructure;

namespace WebApplication1.Application.Services
{
    public class AmountProduct : IAmountProduct
    {
        public Product CheckProducts(Product product,int reqeustNumberOfProducts)
        {
            //превышен лимит существующих продктов для заказа
            if (product.Amount<reqeustNumberOfProducts)
            {
                //ограничелся заглушкой, нужно кидать респонс со статусом и деталями ошибки
                throw new ExhaustiveLimitOfProducts();
            }


            

            return product;
        }
    }
}
