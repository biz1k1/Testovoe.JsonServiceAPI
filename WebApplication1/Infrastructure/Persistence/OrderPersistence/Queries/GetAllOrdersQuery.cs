using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entity;
using WebApplication1.Presentation.Common.DTO.Order;

namespace WebApplication1.Infrastructure.Persistence.OrderPersistence.Queries
{
    public class GetAllOrdersQuery:IRequest<List<OrderResponse>>
    {
        public class GetAllOrdersQueryHandler:IRequestHandler<GetAllOrdersQuery,List<OrderResponse>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public GetAllOrdersQueryHandler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<List<OrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
            {
                var orders = await _dataContext.Order.Include(x => x.Products).AsNoTracking().ToListAsync();

                var ordersResponse = _mapper.Map<List<Order>, List<OrderResponse>>(orders);

                return ordersResponse;
            }
        }
    }
}
