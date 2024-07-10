using DbAccess.Models;
using DataCollectionSystem.Services;
using DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbAccess.ApiModels;

namespace DataCollectionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixationPointsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FixationPointsController> _logger;
        private readonly IFixationHandler _fixationHandler;

        public FixationPointsController(
            ApplicationDbContext context, 
            ILogger<FixationPointsController> logger,
            IFixationHandler fixationHandler)
        {
            _context = context;
            _logger = logger;
            _fixationHandler = fixationHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FixationPoint>>> GetAllFixationPoints()
        {
            return await _context.FixationPoints.AsNoTracking().ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<FixationPoint>> PostFixationData([FromBody] FixationData data)
        {
            var res = await _fixationHandler.ProcessAsync(data);
            var veh = await _context.Vehicles.AsNoTracking().FirstOrDefaultAsync(v => v.StateRegistrationPlate == data.StateRegistrationPlate && v.IsActual);

            if (veh == null && res == null)
            {
                return StatusCode(404);
            }

            return StatusCode(201);
        }
    }
}
