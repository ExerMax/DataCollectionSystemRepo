using Azure.Core.Serialization;
using DataCollectionSystem.Controllers;
using DbAccess;
using DbAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataCollectionSystem.Services
{
    public class MessageSender : IMessageSender
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FixationPointsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageSender(
            ApplicationDbContext context,
            ILogger<FixationPointsController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task SendMessageAsync(int ownerId, double tax)
        {
            var httpClient = _httpClientFactory.CreateClient();
            VehicleOwner vo = _context.VehicleOwners.FirstOrDefault(o => o.Id == ownerId);
            TaxMessage tm = new TaxMessage
            {
                Id = 0,
                Tax = tax,
                Name = vo.Name,
                Surname = vo.Surname,
                Patronymic = vo.Patronymic,
                TaxpayerIdentificationNumber = vo.TaxpayerIdentificationNumber,
                Phone = vo.Phone,
                Address = vo.Address,
                Email = vo.Email,
                Document = vo.Document
            };

            var content = JsonContent.Create(tm);

            var response = await httpClient.PostAsync("https://localhost:7230/api/Messages", content);

            var responseString = await response.Content.ReadAsStringAsync();

            _logger.LogWarning(responseString);

            return;
        }
    }
}
