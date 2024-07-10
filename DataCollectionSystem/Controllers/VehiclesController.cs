using DbAccess.Models;
using DbAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(ApplicationDbContext context, ILogger<VehiclesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpDelete("{srp}")]
        public async Task<ActionResult<Vehicle>> MarkVehicleAsDeleted(string srp)
        {
            Vehicle vehicle = await _context.Vehicles.FirstOrDefaultAsync(r => r.StateRegistrationPlate == srp);

            if (vehicle == null)
            {
                return NotFound();
            }
            else
            {
                vehicle.IsActual = false;

                try
                {
                    _context.Vehicles.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                vehicle = await _context.Vehicles.FirstOrDefaultAsync(r => r.StateRegistrationPlate == srp);

                if (vehicle.IsActual == false)
                {
                    return vehicle;
                }
                else
                {
                    return StatusCode(501);
                }
            }
        }
    }
}