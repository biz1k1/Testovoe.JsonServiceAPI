using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Services.ServiceHandler;
using WebApplication1.Domain.Entity;
using WebApplication1.Domain.Enums;
using WebApplication1.Presentation.Common.DTO.Order;

namespace WebApplication1.Infrastructure.Persistence.OrderPersistence.Commands
{
    public class UpdateOrderCommand:IRequest<Guid>
    {
        public OrderRequestUpdate OrderRequest { get; set; }
        public UpdateOrderCommand(OrderRequestUpdate orderRequest)
        {
            OrderRequest = orderRequest;
        }
        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Guid>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public UpdateOrderCommandHandler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;

            }
            public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _dataContext.Order.FindAsync(request.OrderRequest.OrderId);

                if (order == null)
                {
                    throw new Exception();
                }


                await _dataContext.Order.Where(x => x.Id == request.OrderRequest.OrderId)
                    .ExecuteUpdateAsync(x => x
                    .SetProperty(x => x.OrderStatus, (OrderStatus)Enum.Parse(typeof(OrderStatus), request.OrderRequest.OrderStatus)));
                await _dataContext.SaveChangesAsync();

                return order.Id;
            }
        }
    }
}
