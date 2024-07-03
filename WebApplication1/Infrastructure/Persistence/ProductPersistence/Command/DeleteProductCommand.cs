using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Infrastructure.Persistence.OrderPersistence.Commands;

namespace WebApplication1.Infrastructure.Persistence.ProductPersistence.Command
{
    public class DeleteProductCommand:IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
        {
            private readonly DataContext _dataContext;
            public DeleteProductCommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _dataContext.Product.Include(x=>x.Orders).FirstOrDefaultAsync(x=>x.Id==request.Id);
                if (product == null)
                {
                    throw new Exception(message: "Product not found");
                }

                var order = await _dataContext.Order.Where(x => x.Products.Contains(product)).ToListAsync();
                if (order!=null)
                {
                    throw new Exception(message: "You cannot delete the item that has been ordered");
                }

                _dataContext.Product.Remove(product);
                await _dataContext.SaveChangesAsync();

                return product.Id;
            }
        }
    }
}
