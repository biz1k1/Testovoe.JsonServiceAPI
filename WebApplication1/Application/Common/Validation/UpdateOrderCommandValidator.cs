using FluentValidation;
using WebApplication1.Domain.Enums;
using WebApplication1.Infrastructure.Persistence.OrderPersistence.Commands;

namespace WebApplication1.Application.Common.Validation
{
    public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.OrderRequest.OrderStatus).Must(x=>Enum.IsDefined(typeof(OrderStatus),x)).WithMessage("Значения статуса, которое вы ввели, не существует для поля OrderStatus");
        }
    }
}
