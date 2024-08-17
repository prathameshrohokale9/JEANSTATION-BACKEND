using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeanStationAPI.Models;
using static NuGet.Packaging.PackagingConstants;

namespace JeanStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly JeanStationContext _context;
        public OrdersController(JeanStationContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                var orders = await _context.Orders.ToListAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Orders/5
        [HttpGet("{orderId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByOrderId(int orderId)
        {
            if (orderId <= 0)
            {
                return BadRequest("Invalid order ID.");
            }

            try
            {
                var orders = await _context.Orders
                    .Where(o => o.OrderId == orderId)
                    .ToListAsync();

                if (orders == null || !orders.Any())
                {
                    return NotFound("No orders found for the specified order ID.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Orders/storeId/5
        [HttpGet("storeId/{storeId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByStoreId(int storeId)
        {
            if (storeId <= 0)
            {
                return BadRequest("Invalid store ID.");
            }

            try
            {
                var orders = await _context.Orders
                    .FromSqlRaw(
                        @"SELECT * FROM orders WHERE itemCode IN (SELECT itemCode FROM item WHERE storeId = {0})", storeId)
                    .ToListAsync();

                if (orders == null || !orders.Any())
                {
                    return NotFound("No orders found for the specified store.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Orders/5/ABC
        [HttpGet("{orderId}/{itemCode}")]
        public async Task<ActionResult<Order>> GetOrder(int orderId, string itemCode)
        {
            if (orderId <= 0 || string.IsNullOrEmpty(itemCode))
            {
                return BadRequest("Invalid order ID or item code.");
            }

            try
            {
                var trimmedItemCode = itemCode.Trim();
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.OrderId == orderId && o.ItemCode == trimmedItemCode);

                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Orders/5/ABC
        [HttpPut("{orderId}/{itemCode}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string itemCode, [FromBody] OrderStatusUpdateRequest request)
        {
            if (orderId <= 0 || string.IsNullOrEmpty(itemCode) || string.IsNullOrEmpty(request?.orderStatus))
            {
                return BadRequest("Invalid input data.");
            }

            try
            {
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.OrderId == orderId && o.ItemCode == itemCode.Trim());

                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                order.OrderStatus = request.orderStatus;
                _context.Entry(order).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Concurrency error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        public class OrderStatusUpdateRequest
        {
            public string orderStatus { get; set; }
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromQuery] int custId)
        {
            if (custId <= 0)
            {
                return BadRequest("Invalid customer ID.");
            }

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "INSERT INTO orders (custId) VALUES ({0})",
                    custId
                );

                return Ok("Order creation initiated.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Orders/customer/{custId}
        [HttpGet("customer/{custId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomerId(int custId)

        {

            var orders = await _context.Orders
            .Where(o => o.CustId == custId)
            .ToListAsync();

            if (orders == null || !orders.Any())
            {

                return NotFound();

            }

            return Ok(orders);
        }

        private bool OrderExists(int orderId, string itemCode)
        {
            return _context.Orders.Any(e => e.OrderId == orderId && e.ItemCode == itemCode.Trim());
        }
    }
}