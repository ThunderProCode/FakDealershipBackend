using FakeDealerAPI.Auth;
using FakeDealerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public IEnumerable<Car> GetAll(){
        return _context.Cars;
    }

    [Authorize]
    [HttpPost]
    public void Create(Car car)
    {
        _context.Cars.Add(car);
        _context.SaveChanges();
    }

    [Authorize]
    [HttpDelete]
    public void Delete(int id)
    {
        var car = _context.Cars.Find(id);
        if(car != null)
        {
            _context.Cars.Remove(car);
            _context.SaveChanges();
        }
    }

}