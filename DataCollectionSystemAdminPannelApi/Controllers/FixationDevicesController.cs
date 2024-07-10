using DbAccess.Models;
using DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbAccess.ApiModels;

namespace DataCollectionSystemAdminPannelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixationDevicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FixationDevicesController> _logger;

        public FixationDevicesController(ApplicationDbContext context, ILogger<FixationDevicesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("Page={page}&Quantity={quantity}&TypeFilter={tf}")]
        public async Task<ActionResult<IEnumerable<FixationDevice>>> GetFixationDevicesWithFilters(int page, int quantity, string tf)
        {
            if(tf == "all")
            {
                return await _context.FixationDevices.Skip((page - 1) * quantity).Take(quantity).ToListAsync();
            }
            else
            {
                return await _context.FixationDevices.Where(fd => fd.Type == tf).Skip((page - 1) * quantity).Take(quantity).ToListAsync();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FixationDevice>> MarkFixationDeviceAsDeleted(int id)
        {
            FixationDevice fixationDevice = await _context.FixationDevices.FirstOrDefaultAsync(r => r.Id == id);

            if (fixationDevice == null)
            {
                return NotFound();
            }
            else
            {
                fixationDevice.IsActual = false;

                try
                {
                    _context.FixationDevices.Update(fixationDevice);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                fixationDevice = await _context.FixationDevices.FirstOrDefaultAsync(r => r.Id == id);

                if (fixationDevice.IsActual == false)
                {
                    return fixationDevice;
                }
                else
                {
                    return StatusCode(501);
                }
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<FixationDevice>> RestoreFixationDevice(int id)
        {
            FixationDevice fixationDevice = await _context.FixationDevices.FirstOrDefaultAsync(r => r.Id == id);

            if (fixationDevice == null)
            {
                return NotFound();
            }
            else
            {
                fixationDevice.IsActual = true;

                try
                {
                    _context.FixationDevices.Update(fixationDevice);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                fixationDevice = await _context.FixationDevices.FirstOrDefaultAsync(r => r.Id == id);

                if (fixationDevice.IsActual == true)
                {
                    return fixationDevice;
                }
                else
                {
                    return StatusCode(501);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<FixationDevice>> PostFixationDevice([FromBody] NewFixationDevice newFd)
        {
            FixationDevice fd = new FixationDevice() { Type = newFd.Type, Number = newFd.Number, Address = newFd.Address };
            FixationDevice checkFd = _context.FixationDevices.FirstOrDefault(f => f.Type == fd.Type && f.Number == fd.Number);

            if(checkFd == null)
            {
                _context.FixationDevices.Add(fd);
                await _context.SaveChangesAsync();
            }
            else
            {
                return StatusCode(409, "Already exists");
            }

            return await _context.FixationDevices.OrderBy(fd => fd.Id).LastAsync();
        }
    }
}
