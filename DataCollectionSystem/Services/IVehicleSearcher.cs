using DbAccess.Models;
using Microsoft.AspNetCore.Components.Web;

namespace DataCollectionSystem.Services
{
    public interface IVehicleSearcher
    {
        public Vehicle? FindVehicle(string stateRegistrationPlate);
    }
}
