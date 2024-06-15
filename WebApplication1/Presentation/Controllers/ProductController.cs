using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Application.Services.ServiceHandler;
using WebApplication1.Domain.Entity;
using WebApplication1.Infrastructure;
using WebApplication1.Presentation.Common.DTO_s;
using WebApplication1.Presentation.Common.DTO_s.Product;

namespace WebApplication1.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ProductController(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(template: "GetAll")]
        public async Task<ActionResult> GetAllProduct()
        {
            var product = await _dataContext.Product.AsNoTracking().ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            var productResponse = _mapper.Map<List<ProductResponse>>(product);

            return Ok(productResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(Guid id)
        {
            var product = await _dataContext.Product.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);

            if (product == null)
            {
                return NotFound();
            }

            var productResponse = _mapper.Map<ProductResponse>(product);

            return Ok(productResponse);
        }

        [HttpPost]
        [Route(template: "Create")]
        public async Task<ActionResult> CreateProduct(ProductRequestCreate productRequest)
        {
            var product = await _dataContext.Product.FirstOrDefaultAsync(x=>x.Name==productRequest.Name);

            if (product != null )
            {
                return BadRequest();
            }

            var productEntity = _mapper.Map<Product>(productRequest);

            await _dataContext.Product.AddAsync(productEntity);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route(template: "Update")]
        public async Task<ActionResult> UpdateProduct(ProductRequestUpdate productRequest)
        {
            var product = await _dataContext.Product.FindAsync(productRequest.Id);

            if (product == null)
            {
                return NotFound();
            }   


            await _dataContext.Product
                .Where(x=>x.Id==productRequest.Id)
                .ExecuteUpdateAsync(x=>x
                .SetProperty(prop=>prop.Name,productRequest.Name)
                .SetProperty(prop=>prop.Amount,productRequest.Amount));

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route(template: "Delete")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            var product = await _dataContext.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _dataContext.Product.Remove(product);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
