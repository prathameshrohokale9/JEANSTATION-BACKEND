using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeanStationAPI.Models;

namespace JeanStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly JeanStationContext _context;

        public CartsController(JeanStationContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{custId}/{itemCode}")]
        public async Task<IActionResult> UpdateCartItemQuantity(int custId, string itemCode, [FromQuery] int newQty)
        {
            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.CustId == custId && c.ItemCode == itemCode);

            if (cartItem == null)
            {
                return NotFound();
            }

            // Update quantity
            cartItem.Qty = newQty;

            _context.Entry(cartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(custId,itemCode))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            _context.Carts.Add(cart);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CartExists(cart.CustId,cart.ItemCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCart", new { id = cart.CustId }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{custId}/{itemCode}")]
        public async Task<IActionResult> DeleteCartItem(int custId, string itemCode)
        {
            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.CustId == custId && c.ItemCode == itemCode);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int custId, string itemCode)
        {
            return _context.Carts.Any(e => e.CustId == custId && e.ItemCode == itemCode);
        }
        // GET: api/Carts/customer/5
        [HttpGet("customer/{custId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartByCustomerId(int custId)
        {
            
            var cartItems = await _context.Carts
                .Where(c => c.CustId == custId)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound();
            }

            return Ok(cartItems);
        }
    }
}
