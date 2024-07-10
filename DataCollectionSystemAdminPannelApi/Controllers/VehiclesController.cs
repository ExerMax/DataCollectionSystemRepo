using DbAccess.Models;
using DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionSystemAdminPannelApi.Controllers
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

        [HttpGet("Page={page}&Quantity={quantity}&Num={num}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles(int page, int quantity, string num)
        {
            if(num == "all")
            {
                return await _context.Vehicles
                .Include(v => v.VehicleOwner)
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync();
            }
            else
            {
                return await _context.Vehicles
                .Where(v => v.StateRegistrationPlate == num)
                .Include(v => v.VehicleOwner)
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync();
            }
        }
    }
}
