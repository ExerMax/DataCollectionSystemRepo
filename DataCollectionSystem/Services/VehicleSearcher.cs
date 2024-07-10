using DataCollectionSystem.Controllers;
using DbAccess.Models;
using DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace DataCollectionSystem.Services
{
    public class VehicleSearcher : IVehicleSearcher
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VehicleSearcher> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IExternalSearcher _externalSearcher;

        public VehicleSearcher(
            ApplicationDbContext context, 
            ILogger<VehicleSearcher> logger, 
            IHttpClientFactory httpClientFactory, 
            IExternalSearcher externalSearcher)
        {
            _context = context;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _externalSearcher = externalSearcher;
        }

        public Vehicle? FindVehicle(string stateRegistrationPlate)
        {
            Vehicle veh = _context.Vehicles.AsNoTracking().FirstOrDefault(v => v.StateRegistrationPlate == stateRegistrationPlate && v.IsActual);

            if (veh == null)
            {
                veh = _externalSearcher.FindExternalData(stateRegistrationPlate);
            }
            if (veh == null)
            {
                _logger.LogWarning($"Vehicle with STR = {stateRegistrationPlate} not found");
            }

            return veh;
        }
    }
}
