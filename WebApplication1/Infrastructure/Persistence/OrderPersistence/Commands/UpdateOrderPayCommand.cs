using AutoMapper;
using MediatR;
using WebApplication1.Application.Services.ServiceHandler;
using WebApplication1.Domain.Enums;
using WebApplication1.Presentation.Common.DTO.Order;

namespace WebApplication1.Infrastructure.Persistence.OrderPersistence.Commands
{
    public class UpdateOrderPayCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public UpdateOrderPayCommand(Guid id)
        {
            Id = id;
        }
        public class UpdateOrderPayCommandHandler : IRequestHandler<UpdateOrderPayCommand, Guid>
        {
            private readonly DataContext _dataContext;
            private readonly OrderServiceHandler _orderServiceHandler;
            public UpdateOrderPayCommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;

            }
            public async Task<Guid> Handle(UpdateOrderPayCommand request, CancellationToken cancellationToken)
            {
                var order = await _dataContext.Order.FindAsync(request.Id);

                if (order == null)
                {
                    throw new Exception(message:"Order not found");
                }

                order.OrderStatus = OrderStatus.Completed;
                await _dataContext.SaveChangesAsync();

                return order.Id;
            }
        }
    }
}
