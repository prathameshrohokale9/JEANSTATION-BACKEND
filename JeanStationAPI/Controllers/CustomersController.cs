using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeanStationAPI.Models;
using Microsoft.AspNetCore.Identity.Data;


namespace JeanStationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomersController : ControllerBase
    {
        ILogger<CustomersController> _logger;
        private readonly JeanStationContext _context;

        public CustomersController(JeanStationContext context, ILogger<CustomersController> log)
        {
            _logger = log;
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            //_logger.LogInformation("get customers called" + DateTime.Now);
            /*var customers = await _context.Customers.ToListAsync();
            return Ok(customers);*/
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            //_logger.LogInformation("get customer with ID called" + DateTime.Now);
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, [FromBody] Customer customer)
        {


            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null)
            {
                return NotFound("Customer not found.");
            }

            // Update properties
            existingCustomer.CustName = customer.CustName;
            existingCustomer.Address = customer.Address;
            existingCustomer.PhoneNo = customer.PhoneNo;
            existingCustomer.Email = customer.Email;
            existingCustomer.UserName = customer.UserName;
            existingCustomer.Pwd = customer.Pwd;

            _context.Entry(existingCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound("Customer not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (customer == null) { return BadRequest(); }
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("login")]
        public async Task<ActionResult<Customer>> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Pwd))
            {
                return BadRequest("Username and password are required.");
            }
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserName == request.Username);

            if (customer == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            if (customer.Pwd != request.Pwd)
            {
                return Unauthorized("Invalid username or password.");
            }
            return customer;
        }
        public class LoginRequest
        {
            public string Username { get; set; } = null!; // Make sure to handle nullability
            public string Pwd { get; set; } = null!; // Same here for Password
        }


        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustId == id);
        }
    }
}
