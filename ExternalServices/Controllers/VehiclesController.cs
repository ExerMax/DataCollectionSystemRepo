using DbAccess.Models;
using DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbAccess.ApiModels;

namespace ExternalServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ExternalServicesDbContext _context;
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(ExternalServicesDbContext context, ILogger<VehiclesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllVehicles()
        {
            return await _context.Vehicles.Include(v => v.VehicleOwner).ToListAsync();
        }

        [HttpGet("{stateRegistrationPlate}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(string stateRegistrationPlate)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.StateRegistrationPlate == stateRegistrationPlate);
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle([FromBody] NewVehicle data)
        {
            try
            {
                VehicleOwner vo = _context.VehicleOwners.FirstOrDefault(i => i.Id == data.VehicleOwnerId);
                _context.Vehicles.Add(new Vehicle
                {
                    StateRegistrationPlate = data.StateRegistrationPlate,
                    IsTruck = data.IsTruck,
                    VehicleOwner = vo
                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return StatusCode(201, data);
        }
    }
}
