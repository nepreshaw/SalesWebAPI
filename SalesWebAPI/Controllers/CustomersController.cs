using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebAPI.Models;

namespace SalesWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //controller base allow us to work with the models and controllers, and send and recieve json data
    public class CustomersController : ControllerBase
    {

        private readonly AppDbContext _context;

        //takes one parameter(context and stores in _context
        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        //http get just reads the database
        //IEnumerable gets back a collection of customers
        //action result returns data to the caller and other things if no data is found 
        //task is a class that exists for web applications, asynchronus application
        //asynchronus- 
        //synchronus- 
        //async identifies as an asynchronus method
        //async must be wrapped in a task
        //await and ToListAsync is required
        //all methods will be async when vs generates the controller
        //capstone will need to be marked async and wrapped in task
        //public methods will be async

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        //get is read
        //similar to get by pk
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {   //cant return null on the web
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //put is like an update or an add
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //post is like an add
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {   //add does not hit the database so no need for await
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
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

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
