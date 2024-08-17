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
    public class ItemsController : ControllerBase
    {
        private readonly JeanStationContext _context;

        public ItemsController(JeanStationContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(string id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet("itemName/{itemName}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByItemName(string itemName)
        {
            // Use EF.Functions.Like to perform a case-insensitive pattern search
            var items = await _context.Items
                .Where(i => EF.Functions.Like(i.ItemName, $"%{itemName}%"))
                .ToListAsync();

            if (items == null || !items.Any())
            {
                return NotFound();
            }

            return Ok(items);
        }
        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByStoreId(int storeId)
        {
            var items = await _context.Items
                .Where(i => i.StoreId == storeId)
                .ToListAsync();

            if (items == null || !items.Any())
            {
                return NotFound();
            }

            return Ok(items);
        }



        [HttpPost("priceRange")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByPriceRange([FromBody] PriceRangeRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }

            var items = await _context.Items
                .Where(i => i.Price >= request.MinPrice && i.Price <= request.MaxPrice)
                .ToListAsync();

            if (items == null || !items.Any())
            {
                return NotFound();
            }

            return Ok(items);
        }

        public class PriceRangeRequest
        {
            public double MinPrice { get; set; }
            public double MaxPrice { get; set; }
        }


        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(string id, Item item)
        {
            if (id != item.ItemCode)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.Items.Add(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemExists(item.ItemCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetItem", new { id = item.ItemCode }, item);
        }
        /*[HttpGet("itemName/{itemName}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByitemName(string itemName)
        {
            var items = await _context.Items
                .Where(i => i.ItemName == itemName)
                .ToListAsync();

            if (items == null || !items.Any())
            {
                return NotFound();
            }

            return Ok(items);
        }
*/
        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(string id)
        {
            return _context.Items.Any(e => e.ItemCode == id);
        }
    }
}
