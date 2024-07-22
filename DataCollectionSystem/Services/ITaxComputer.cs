using DbAccess.Models;

namespace DataCollectionSystem.Services
{
    public interface ITaxComputer
    {
        public double Compute(Road road);
    }
}
