using FakeDealerAPI.Auth;
using FakeDealerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeDealerAPI.Controllers;

[Route("api/Orders")]
[ApiController]
public class OrderController: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await _context.Orders.ToListAsync();
        return Ok(orders);
    }

    // Get: api/Orders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if(order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    //Post: api/Orders
    [HttpPost]
    public async Task<IActionResult<Order>> PostOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetOrder", new {id = order.Id}, order);
    }

    //PUT: api/Orders/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, Order order)
    {
        if(id != order.Id)
        {
            return BadRequest();
        }

        _context.Entry(order).State = EntityState.Modified;
        try {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if(!OrderExists(id))
            {
                return NotFound();
            } else {
                throw;
            }
        }
        return NoContent();
    }

    // DELETE: api/Orders/8
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {  
        var order = await _context.Orders.FindAsync(id);
        if(order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool OrderExists(int id)
    {
        return _context.Orders.Any(e => e.Id == id);
    }

}