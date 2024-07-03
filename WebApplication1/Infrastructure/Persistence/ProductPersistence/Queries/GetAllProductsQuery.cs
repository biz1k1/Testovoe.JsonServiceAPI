using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entity;
using WebApplication1.Presentation.Common.DTO.Product;

namespace WebApplication1.Infrastructure.Persistence.ProductPersistence.Queries
{
    public class GetAllProductsQuery:IRequest<List<ProductResponse>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public GetAllProductsQueryHandler(DataContext dataContext,IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }
            public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var products = await _dataContext.Product.AsNoTracking().ToListAsync();

                return _mapper.Map<List<ProductResponse>>(products);
            }
        }
    }
}
