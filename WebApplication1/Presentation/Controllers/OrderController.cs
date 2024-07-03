using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entity;
using WebApplication1.Domain.Enums;
using WebApplication1.Presentation.Common.DTO.Order;
using WebApplication1.Infrastructure.Persistence.OrderPersistence;
using MediatR;
using WebApplication1.Infrastructure.Persistence.OrderPersistence.Commands;
using WebApplication1.Infrastructure.Persistence.OrderPersistence.Queries;

namespace WebApplication1.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrders()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllOrdersQuery()));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderById(Guid id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetOrderByIdQuery(id)));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderRequestCreate orderRequestCreate)
        {
            try
            {
                return Ok(await _mediator.Send(new CreateOrderCommand(orderRequestCreate)));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // Контроллер для оплаты заказов

        [HttpPut]
        [Route(template: "Pay")]
        public async Task<ActionResult> CalculateOrder(Guid id)
        {
            try
            {
                await _mediator.Send(new UpdateOrderPayCommand(id));

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrder(OrderRequestUpdate orderRequestUpdate)
        {
            try
            {
                await _mediator.Send(new UpdateOrderCommand(orderRequestUpdate));

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            try
            {
            await _mediator.Send(new DeleteOrderCommand(id));

            return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route(template: "Product")]
        public async Task<ActionResult> DeleteProductFromOrder(OrderReqeustDelete orderReqeustDelete)
        {
            try
            {
            await _mediator.Send(new DeleteProductFromOrder(orderReqeustDelete));

            return Ok();

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

