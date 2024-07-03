using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApplication1.Application.Services.ServiceHandler;
using WebApplication1.Application.Services.StatusOrder;
using WebApplication1.Domain.Entity;
using WebApplication1.Domain.Enums;
using WebApplication1.Presentation.Common.DTO.Order;

namespace WebApplication1.Infrastructure.Persistence.OrderPersistence.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public OrderRequestCreate OrderRequest { get; set; }
        public CreateOrderCommand(OrderRequestCreate order)
        {
            OrderRequest = order;
        }
        public class CreateOrderCommandHandler:IRequestHandler<CreateOrderCommand,Guid>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            private readonly OrderServiceHandler _orderServiceHandler;
            public CreateOrderCommandHandler(DataContext dataContext,IMapper mapper, OrderServiceHandler orderServiceHandler)
            {
                _dataContext = dataContext;
                _mapper = mapper;
                _orderServiceHandler = orderServiceHandler;


            }

            public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
            {
                var product = await _dataContext.Product.FindAsync(command.OrderRequest.ProductId);
                var order = await _dataContext.Order.FindAsync(command.OrderRequest.OrderId);
                if (product == null)
                {
                    throw new Exception(message:"Product not found");
                }

                var verifiedProduct = _orderServiceHandler.CheckExistProductsAndAddThemToOrder(product, 1);

                product.Amount -= 1;
                if (order == null)
                {
                    var orderEntity = _mapper.Map<Order>(command.OrderRequest);
                    orderEntity.Products.Add(verifiedProduct);

                    orderEntity.OrderStatus = OrderStatus.Drafted;

                    await _dataContext.Order.AddAsync(orderEntity);
                    await _dataContext.SaveChangesAsync();


                    return orderEntity.Id;
                }
                else 
                {
                    if (order.OrderStatus==OrderStatus.Closed)
                    {
                        throw new Exception(message:"The order is closed ");
                    }
                    order.Products.Add(verifiedProduct);
                    await _dataContext.SaveChangesAsync();
                }

                return order.Id;
            }

        }
    }
}
