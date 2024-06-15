using WebApplication1.Domain.Entity;
using WebApplication1.Infrastructure;

namespace WebApplication1.Application.Services
{
    public interface IAmountProduct
    {
        public Product CheckProducts(Product product, int numberOfproducts);
    }
}
