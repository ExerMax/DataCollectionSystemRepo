using DbAccess.Models;

namespace DataCollectionSystem.Services
{
    public interface IDbWriter
    {
        public bool WriteToDb(VehicleOwner vo, Vehicle veh);
    }
}
