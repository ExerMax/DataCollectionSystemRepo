using DbAccess.Models;

namespace DataCollectionSystem.Services
{
    public interface IExternalSearcher
    {
        public Vehicle? FindExternalData(string stateRegistrationPlate);
    }
}
