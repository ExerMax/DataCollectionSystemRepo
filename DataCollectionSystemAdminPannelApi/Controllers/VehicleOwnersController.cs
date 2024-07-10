using DbAccess.Models;
using DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionSystemAdminPannelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleOwnersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VehicleOwnersController> _logger;

        public VehicleOwnersController(ApplicationDbContext context, ILogger<VehicleOwnersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("Page={page}&Quantity={quantity}&TIN={tin}")]
        public async Task<ActionResult<IEnumerable<VehicleOwner>>> GetVehicleOwners(int page, int quantity, string tin)
        {
            if(tin == "all")
            {
                return await _context.VehicleOwners.Skip(1).Skip((page - 1) * quantity).Take(quantity).ToListAsync();
            }
            else
            {
                return await _context.VehicleOwners.Where(vo => vo.TaxpayerIdentificationNumber == tin).Skip(1).Skip((page - 1) * quantity).Take(quantity).ToListAsync();
            }
        }
    }
}
