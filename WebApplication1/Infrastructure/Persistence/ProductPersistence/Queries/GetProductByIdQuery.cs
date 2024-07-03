using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Infrastructure.Persistence.OrderPersistence.Queries;
using WebApplication1.Presentation.Common.DTO.Order;
using WebApplication1.Presentation.Common.DTO.Product;

namespace WebApplication1.Infrastructure.Persistence.ProductPersistence.Queries
{
    public class GetProductByIdQuery:IRequest<ProductResponse>
    {
        public Guid Id { get; set; }
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public GetProductByIdQueryHandler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }
            public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _dataContext.Product.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

                if (product == null)
                {
                    throw new Exception(message: "Product not found");
                }

                return _mapper.Map<ProductResponse>(product);
            }
        }
    }
}
