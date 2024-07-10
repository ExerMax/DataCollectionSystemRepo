using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbAccess.Models;
using DbAccess;
using static DataCollectionSystemAdminPannelApi.Controllers.FixationDevicesController;
using DbAccess.ApiModels;

namespace DataCollectionSystemAdminPannelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RoadsController> _logger;

        public RoadsController(ApplicationDbContext context, ILogger<RoadsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("Page={page}&Quantity={quantity}")]
        public async Task<ActionResult<IEnumerable<Road>>> GetRoads(int page, int quantity)
        {
            return await _context.Roads
                .Include(r => r.StartPoint)
                .Include(r => r.EndPoint)
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Road>> MarkRoadAsDeleted(int id)
        {
            Road road = await _context.Roads.FirstOrDefaultAsync(r => r.Id == id);

            if(road == null)
            {
                return NotFound();
            }
            else
            {
                road.IsActual = false;

                try
                {
                    _context.Roads.Update(road);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                road = await _context.Roads.FirstOrDefaultAsync(r => r.Id == id);

                if (road.IsActual == false)
                {
                    return road;
                }
                else
                {
                    return StatusCode(501);
                }
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Road>> RestoreRoad(int id)
        {
            Road road = await _context.Roads.FirstOrDefaultAsync(r => r.Id == id);

            if (road == null)
            {
                return NotFound();
            }
            else
            {
                road.IsActual = true;

                try
                {
                    _context.Roads.Update(road);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                road = await _context.Roads.FirstOrDefaultAsync(r => r.Id == id);

                if (road.IsActual == true)
                {
                    return road;
                }
                else
                {
                    return StatusCode(501);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<Road>> PostFixationDevice([FromBody] NewRoad newRoad)
        {
            FixationDevice fd1 = _context.FixationDevices.FirstOrDefault(fd => fd.Type == newRoad.StartPointType && fd.Number == newRoad.StartPointNumber);
            FixationDevice fd2 = _context.FixationDevices.FirstOrDefault(fd => fd.Type == newRoad.EndPointType && fd.Number == newRoad.EndPointNumber);

            if(fd1 == null || fd2 == null)
            {
                return StatusCode(404, "Fixation devices not found");
            }
            else
            {
                Road road = new Road() { StartPointId = fd1.Id, EndPointId = fd2.Id, Value = newRoad.Value};

                Road checkRoad = _context.Roads.FirstOrDefault(r => r.StartPointId == road.StartPointId && r.EndPointId == road.EndPointId);

                if (checkRoad == null)
                {
                    _context.Roads.Add(road);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return StatusCode(409, "Already exists");
                }
            }

            return await _context.Roads.OrderBy(fd => fd.Id).LastAsync();
        }
    }
}
