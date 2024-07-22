using DataCollectionSystem.Controllers;
using DbAccess.Models;
using DbAccess;

namespace DataCollectionSystem.Services
{
    public class TaxComputer : ITaxComputer
    {
        private readonly ILogger<TaxComputer> _logger;
        private readonly double _feePerKilo = 3.05D;

        public TaxComputer(ILogger<TaxComputer> logger)
        {
            _logger = logger;
        }

        public double Compute(Road road)
        {
            if(road == null) 
            {
                _logger?.LogWarning("Cannot compute: road is null");
                return 0; 
            }
            else
            {
                return road.Value * _feePerKilo;
            }
        }
    }
}
