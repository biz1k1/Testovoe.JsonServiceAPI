using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Presentation.Common.DTO.Order;

namespace WebApplication1.Infrastructure.Persistence.OrderPersistence.Queries
{
    public class GetOrderByIdQuery:IRequest<OrderResponse>
    {
        public Guid Id { get; set; }
        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public GetOrderByIdQueryHandler(DataContext dataContext,IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }
            public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _dataContext.Order.Include(x => x.Products).AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

                if (order == null)
                {
                    throw new Exception(message: "Order not found");
                }

                var orderResponse = _mapper.Map<OrderResponse>(order);

                return orderResponse;
            }
        }
    }
}
