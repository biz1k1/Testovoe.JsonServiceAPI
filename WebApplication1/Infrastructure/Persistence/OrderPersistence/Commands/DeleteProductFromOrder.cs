using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Presentation.Common.DTO.Order;

namespace WebApplication1.Infrastructure.Persistence.OrderPersistence.Commands
{
    public class DeleteProductFromOrder:IRequest<Guid>
    {
        public OrderReqeustDelete OrderReqeustDelete { get; set; }
        public DeleteProductFromOrder(OrderReqeustDelete orderReqeustDelete)
        {
            OrderReqeustDelete = orderReqeustDelete;
        }
        public class DeleteProductFromOrderHandler : IRequestHandler<DeleteProductFromOrder, Guid>
        {
            private readonly DataContext _dataContext;
            public DeleteProductFromOrderHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<Guid> Handle(DeleteProductFromOrder request, CancellationToken cancellationToken)
            {
                var order = await _dataContext.Order.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == request.OrderReqeustDelete.OrderId);

                if (order == null)
                {
                    throw new Exception(message: "Order not found");
                }

                var product = order.Products.Find(x => x.Id == request.OrderReqeustDelete.ProductId);

                if (product == null)
                {
                    throw new Exception(message: "Prodct not found");
                }

                order.Products.Remove(product);

                if (order.Products.Count() == 0)
                {
                    _dataContext.Order.Remove(order);
                }
                await _dataContext.SaveChangesAsync();

                return order.Id;
            }
        }
    }
}
