using FluentValidation;
using WebApplication1.Infrastructure.Persistence.ProductPersistence.Command;

namespace WebApplication1.Application.Common.Validation
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(command => command.ProductRequest.Amount).NotEmpty().WithMessage("Поле Amount не может быть пустым");
            RuleFor(command => command.ProductRequest.Amount).GreaterThan(0).WithMessage("Поле Amount должно быть больше 0");
            RuleFor(command => command.ProductRequest.Name).MaximumLength(20).WithMessage("Поле Name не может превышать 20 символов");
        }
    }
}
