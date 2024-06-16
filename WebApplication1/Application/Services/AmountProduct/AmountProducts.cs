using WebApplication1.Application.Common.Exceptions;
using WebApplication1.Domain.Entity;

namespace WebApplication1.Application.Services
{
    // Изначально сервис должен был добавлять в заказ определенное количество продуктов
    // и удалять из общего пула продуктов купленные продукты.
   // По итогу в заказе будут общее количество продуктов, а не купленное.
    
    // Наверно, это можно испрвить, создав третю таблицу с айди заказа, айди продукта и с кол-во купленных товаров и хранить данные там
    public class AmountProducts : IAmountProducts
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
