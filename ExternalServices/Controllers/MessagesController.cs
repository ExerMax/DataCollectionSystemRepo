using DbAccess.Models;
using DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExternalServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ExternalServicesDbContext _context;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ExternalServicesDbContext context, ILogger<MessagesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxMessage>>> GetAllMessagesAsync()
        {
            return await _context.TaxMessages.AsNoTracking().ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TaxMessage>> GetMessageAsync(int id)
        {
            return await _context.TaxMessages.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<TaxMessage>> PostMessage([FromBody] TaxMessage data)
        {
            _context.TaxMessages.Add(new TaxMessage
            {
                Tax = data.Tax,
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
            return StatusCode(201);
        }
    }
}
