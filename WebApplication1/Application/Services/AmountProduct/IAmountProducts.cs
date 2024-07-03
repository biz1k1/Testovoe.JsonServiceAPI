using WebApplication1.Domain.Entity;

namespace WebApplication1.Application.Services.AmountProduct
{
    public interface IAmountProducts
    {
        public Product CheckProducts(Product product, int numberOfproducts);
    }
}
