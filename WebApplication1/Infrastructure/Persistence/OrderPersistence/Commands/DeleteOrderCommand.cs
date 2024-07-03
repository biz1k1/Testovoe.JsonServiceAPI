using MediatR;

namespace WebApplication1.Infrastructure.Persistence.OrderPersistence.Commands
{
    public class DeleteOrderCommand:IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }
        public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Guid>
        {
            private readonly DataContext _dataContext;
            public DeleteOrderCommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<Guid> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _dataContext.Order.FindAsync(request.Id);

                if (order == null)
                {
                    throw new Exception(message: "Order not found");
                }

                _dataContext.Order.Remove(order);
                await _dataContext.SaveChangesAsync();

                return order.Id;
            }
        }
    }
}
