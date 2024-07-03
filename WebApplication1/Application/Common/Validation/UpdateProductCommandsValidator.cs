using FluentValidation;
using WebApplication1.Infrastructure.Persistence.ProductPersistence.Command;

namespace WebApplication1.Application.Common.Validation
{
    public class UpdateProductCommandsValidator:AbstractValidator<UpdateProductCommands>
    {
        public UpdateProductCommandsValidator()
        {
            RuleFor(x => x.ProductRequest.Amount).NotEmpty().WithMessage("Поле Amount не может быть пустым");
            RuleFor(x => x.ProductRequest.Amount).GreaterThan(0).WithMessage("Поле Amount должно быть больше 0");

            RuleFor(x => x.ProductRequest.Name).MaximumLength(20).WithMessage("Поле Name не может превышать 20 символов");
        }
    }
}
