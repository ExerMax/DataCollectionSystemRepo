using DbAccess;
using DbAccess.Models;
using Newtonsoft.Json;

namespace DataCollectionSystem.Services
{
    public class ExternalSearcher : IExternalSearcher
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExternalSearcher> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDbWriter _dbWriter;

        public ExternalSearcher(ApplicationDbContext context, ILogger<ExternalSearcher> logger, IHttpClientFactory httpClientFactory, IDbWriter dbWriter)
        {
            _context = context;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _dbWriter = dbWriter;   
        }

        public Vehicle? FindExternalData(string stateRegistrationPlate)
        {
            Vehicle? veh = FindVehicleExternal(stateRegistrationPlate);
            VehicleOwner? vo = null;
            if (veh != null)
            {
                vo = FindVehicleOwnerExternal(veh.VehicleOwnerId);
            }

            _dbWriter.WriteToDb(vo, veh);

            return _context.Vehicles.FirstOrDefault(v => v.StateRegistrationPlate == stateRegistrationPlate && v.IsActual);
        }

        private Vehicle? FindVehicleExternal(string stateRegistrationPlate)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7230/api/Vehicles/{stateRegistrationPlate}");
                HttpResponseMessage response = client.Send(request);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Vehicle>(response.Content.ReadAsStringAsync().Result);
                }
                else return null;
            }
        }

        private VehicleOwner? FindVehicleOwnerExternal(int id)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7230/api/VehicleOwners/{id}");
                HttpResponseMessage response = client.Send(request);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<VehicleOwner>(response.Content.ReadAsStringAsync().Result);
                }
                else return null;
            }
        }
    }
}
