using DbAccess.Models;
using DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionSystemAdminPannelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixationPointsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FixationPointsController> _logger;

        public FixationPointsController(ApplicationDbContext context, ILogger<FixationPointsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("Page={page}&Quantity={quantity}&Number={num}")]
        public async Task<ActionResult<IEnumerable<FixationPoint>>> GetFixationPoints(int page, int quantity, string num)
        {
            if(num == "all")
            {
                return await _context.FixationPoints
                .Include(fp => fp.FixationDevice)
                .Include(fp => fp.Vehicle)
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync();
            }
            else
            {
                return await _context.FixationPoints
                .Include(fp => fp.FixationDevice)
                .Include(fp => fp.Vehicle)
                .Where(fp => fp.Vehicle.StateRegistrationPlate == num)
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync();
            }
        }
    }
}
