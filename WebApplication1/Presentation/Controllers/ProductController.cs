using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entity;
using WebApplication1.Infrastructure;
using WebApplication1.Infrastructure.Persistence.ProductPersistence.Command;
using WebApplication1.Infrastructure.Persistence.ProductPersistence.Queries;
using WebApplication1.Presentation.Common.DTO.Product;

namespace WebApplication1.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediatR)
        {
            _mediator = mediatR;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProduct()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllProductsQuery()));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(Guid id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetProductByIdQuery(id)));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductRequestCreate productRequest)
        {
            try
            {
                return Ok(await _mediator.Send(new CreateProductCommand(productRequest)));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(ProductRequestUpdate productRequest)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateProductCommands(productRequest)));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteProductCommand(id));
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
