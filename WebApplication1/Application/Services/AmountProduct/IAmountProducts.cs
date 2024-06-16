using WebApplication1.Domain.Entity;

namespace WebApplication1.Application.Services
{
    public interface IAmountProducts
    {
        public Product CheckProducts(Product product, int numberOfproducts);
    }
}
