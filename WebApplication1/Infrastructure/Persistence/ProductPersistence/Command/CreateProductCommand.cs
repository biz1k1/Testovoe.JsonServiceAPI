using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication1.Application.Services.ServiceHandler;
using WebApplication1.Domain.Entity;
using WebApplication1.Domain.Enums;
using WebApplication1.Infrastructure.Persistence.OrderPersistence.Commands;
using WebApplication1.Presentation.Common.DTO.Product;

namespace WebApplication1.Infrastructure.Persistence.ProductPersistence.Command
{
    public class CreateProductCommand:IRequest<Guid>
    {
        public ProductRequestCreate ProductRequest { get; set; }
        public CreateProductCommand(ProductRequestCreate productRequest)
        {
            ProductRequest = productRequest;
        }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public CreateProductCommandHandler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;


            }

            public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _dataContext.Product.FirstOrDefaultAsync(x => x.Name == request.ProductRequest.Name);

                if (product != null)
                {
                    throw new Exception(message:"Duplicate product");
                }

                var productEntity = _mapper.Map<Product>(request.ProductRequest);

                await _dataContext.Product.AddAsync(productEntity);
                await _dataContext.SaveChangesAsync();

                return productEntity.Id;
            }

        }
    }
}
