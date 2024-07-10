using DbAccess.Models;
using DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbAccess.ApiModels;

namespace ExternalServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleOwnersController : ControllerBase
    {
        private readonly ExternalServicesDbContext _context;
        private readonly ILogger<VehicleOwnersController> _logger;

        public VehicleOwnersController(ExternalServicesDbContext context, ILogger<VehicleOwnersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleOwner>>> GetAllVehicleOwners()
        {
            return await _context.VehicleOwners.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleOwner>> GetVehicleOwner(int id)
        {
            return await _context.VehicleOwners.FirstOrDefaultAsync(vo => vo.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<NewVehicleOwner>> PostVehicle([FromBody] NewVehicleOwner data)
        {
            try
            {
                _context.VehicleOwners.Add(new VehicleOwner
                {
                    Name = data.Name,
                    Surname = data.Surname,
                    Patronymic = data.Patronymic,
                    TaxpayerIdentificationNumber = data.TaxpayerIdentificationNumber,
                    Phone = data.Phone,
                    Address = data.Address,
                    Email = data.Email,
                    Document = data.Document
                });
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return StatusCode(201, data);
        }
    }
}
