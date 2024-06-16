using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entity;
using WebApplication1.Domain.Enums;
using WebApplication1.Infrastructure;
using WebApplication1.Presentation.Common.DTO;
using WebApplication1.Application.Services.ServiceHandler;

namespace WebApplication1.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly OrderServiceHandler _orderServiceHandler;
        public OrderController(DataContext dataContext,IMapper mapper,OrderServiceHandler orderServiceHandler)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _orderServiceHandler = orderServiceHandler;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrders()
        {
            var orders = await _dataContext.Order.Include(x=>x.Products).AsNoTracking().ToListAsync();


            if (orders == null)
            {
                return NotFound();
            }

            var ordersResponse = _mapper.Map<List<Order>,List<OrderResponse>>(orders);

            return Ok(ordersResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderById(Guid id)
        {
            var order = await _dataContext.Order.Include(x => x.Products).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderResponse = _mapper.Map<OrderResponse>(order);

            return Ok(orderResponse);
        }

        // The number of items purchased is removed from the general product list.
        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderRequestCreate orderRequest)
        {

            var product = await _dataContext.Product.FirstOrDefaultAsync(x=>x.Id==orderRequest.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var verifiedProduct=_orderServiceHandler.CheckExistProductsAndAddThemToOrder(product,orderRequest.AmountProduct);
            
            var orderEntity=_mapper.Map<Order>(orderRequest);
            orderEntity.OrderStatus =OrderStatus.Drafted;


            orderEntity.Products.Add(verifiedProduct);


            await _dataContext.Order.AddAsync(orderEntity);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
        // The controller for processing orders. We connect to the bank's service to check the details and pay for the order.

        [HttpPut]
        [Route(template: "Calculate")]
        public async Task<ActionResult> CalculateOrder(PaymentOrder paymentOrder)
        {
            var order = await _dataContext.Order.FindAsync(paymentOrder.OrderId);

            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = OrderStatus.Completed;
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrder(OrderRequestUpdate orderRequest)
        {
            var order = await _dataContext.Order.FindAsync(orderRequest.OrderId);

            if (order == null)
            {
                return NotFound();
            }
            var orderEntity = _mapper.Map<Order>(orderRequest);

            _orderServiceHandler.CheckStatusOrder(orderRequest.OrderStatus);


            await _dataContext.Order.Where(x=>x.Id==orderRequest.OrderId)
                .ExecuteUpdateAsync(x=>x
                .SetProperty(x=>x.OrderStatus, (OrderStatus)Enum.Parse(typeof(OrderStatus), orderRequest.OrderStatus)));
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var order = await _dataContext.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            _dataContext.Order.Remove(order);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete]
        [Route(template: "Product")]
        public async Task<ActionResult> DeleteProductFromOrder(OrderReqeustDelete orderReqeustDelete)
        {
            var order = await _dataContext.Order.Include(x=>x.Products).FirstOrDefaultAsync(x=>x.Id==orderReqeustDelete.OrderId);

            if (order == null)
            {
                return NotFound();
            }

            var product = order.Products.Find(x=>x.Id==orderReqeustDelete.ProductId);

            if (product == null)
            {
                return NotFound();
            }

            order.Products.Remove(product);

            _dataContext.Update(order);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}

