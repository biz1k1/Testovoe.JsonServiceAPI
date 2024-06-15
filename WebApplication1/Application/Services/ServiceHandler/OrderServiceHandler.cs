using WebApplication1.Domain.Entity;
using WebApplication1.Infrastructure;

namespace WebApplication1.Application.Services.ServiceHandler
{
    // CheckStatusOrder()- Сервис,чтобы при обновление заказа вручную не ввели несуществующий статус.
    // CheckExistProductsForOrder()- смотрит, есть ли на складе нужное количество товара для завершения заказа
    public class OrderServiceHandler
    {
        private readonly IStatusOrder _statusOrder;
        private readonly IAmountProduct _amountProduct;
        public OrderServiceHandler(DataContext dataContext,IStatusOrder statusOrder, IAmountProduct amountProduct)
        {
            _statusOrder = statusOrder;

            _amountProduct = amountProduct;
        }

        public void CheckStatusOrder(string status)
        {
            _statusOrder.CheckStatusOrder(status);
        }

        public Product CheckExistProductsAndAddThemToOrder(Product product, int NumberOfProducts)
        {
            return _amountProduct.CheckProducts(product,NumberOfProducts);
        }
    }
}
