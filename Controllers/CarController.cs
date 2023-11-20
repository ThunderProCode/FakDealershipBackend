using FakeDealerAPI.Auth;
using FakeDealerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeDealerAPI.Controllers;

[Route("api/Cars")]
[ApiController]
public class CarController: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CarController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var cars = await _context.Cars
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return Ok(cars);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] Car car)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] Car updatedCar)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingCar = await _context.Cars.FindAsync(id);

        if (existingCar == null)
        {
            return NotFound();
        }

        existingCar.Make = updatedCar.Make ?? existingCar.Make;
        existingCar.Model = updatedCar.Model ?? existingCar.Model;
        existingCar.Year = updatedCar.Year ?? existingCar.Year;
        existingCar.Mileage = updatedCar.Mileage ?? existingCar.Mileage;
        existingCar.Mpg = updatedCar.Mpg ?? existingCar.Mpg;
        existingCar.Description = updatedCar.Description ?? existingCar.Description;
        existingCar.Price = updatedCar.Price;
        existingCar.VIN = updatedCar.VIN ?? existingCar.VIN;
        existingCar.Images = updatedCar.Images ?? existingCar.Images;
        existingCar.Body = updatedCar.Body ?? existingCar.Body;
        existingCar.Features = updatedCar.Features ?? existingCar.Features;
        existingCar.Seats = updatedCar.Seats ?? existingCar.Seats;
        existingCar.Color = updatedCar.Color ?? existingCar.Color;
        existingCar.Engine = updatedCar.Engine ?? existingCar.Engine;
        
        // Update other properties as needed...

        await _context.SaveChangesAsync();

        return Ok();
    }
    

}