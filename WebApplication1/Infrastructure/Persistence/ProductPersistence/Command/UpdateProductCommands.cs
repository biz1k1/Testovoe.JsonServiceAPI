using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Services.ServiceHandler;
using WebApplication1.Infrastructure.Persistence.OrderPersistence.Commands;
using WebApplication1.Presentation.Common.DTO.Product;

namespace WebApplication1.Infrastructure.Persistence.ProductPersistence.Command
{
    public class UpdateProductCommands:IRequest<Guid>
    {
        public ProductRequestUpdate ProductRequest { get; set; }
        public UpdateProductCommands(ProductRequestUpdate productRequest)
        {
            ProductRequest = productRequest;
        }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommands, Guid>
        {
            
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public UpdateProductCommandHandler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;

            }
            public async Task<Guid> Handle(UpdateProductCommands request, CancellationToken cancellationToken)
            {
                var product = await _dataContext.Product.FindAsync(request.ProductRequest.Id);

                if (product == null)
                {
                    throw new Exception(message: "Product not found");
                }


                await _dataContext.Product
                    .Where(x => x.Id == request.ProductRequest.Id)
                    .ExecuteUpdateAsync(x => x
                    .SetProperty(prop => prop.Name, request.ProductRequest.Name)
                    .SetProperty(prop => prop.Amount, request.ProductRequest.Amount));

                await _dataContext.SaveChangesAsync();

                return product.Id;
            }
        }
    }
}
