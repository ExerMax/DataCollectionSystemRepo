using DataCollectionSystem.Controllers;
using DbAccess.Models;
using DbAccess;
using Microsoft.AspNetCore.Mvc;
using DbAccess.ApiModels;

namespace DataCollectionSystem.Services
{
    public class FixationHandler : IFixationHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FixationPointsController> _logger;
        private readonly IVehicleSearcher _vehicleSearcher;
        private readonly ITaxComputer _taxComputer;
        private readonly IMessageSender _messageSender;

        public FixationHandler(
            ApplicationDbContext context,
            ILogger<FixationPointsController> logger,
            IVehicleSearcher vehicleSearcher,
            ITaxComputer taxComputer,
            IMessageSender messageSender)
        {
            _context = context;
            _logger = logger;
            _vehicleSearcher = vehicleSearcher;
            _taxComputer = taxComputer;
            _messageSender = messageSender;
        }

        public async Task<ActionResult<FixationPoint>> ProcessAsync(FixationData data)
        {
            Vehicle veh = _vehicleSearcher.FindVehicle(data.StateRegistrationPlate);

            if (veh == null) return null;
            if (veh.IsTruck == false)   return null;

            FixationDevice fd = _context.FixationDevices.FirstOrDefault(device => device.Number == data.Number && device.Type == data.Type && device.IsActual);

            FixationPoint nfp = new FixationPoint
            {
                Vehicle = veh,
                FixationDevice = fd,
                DateTime = data.DateTime
            };

            FixationPoint lastFixationPoint = _context.FixationPoints.OrderBy(lfp => lfp.Id).LastOrDefault(lfp => lfp.VehicleId == veh.Id);

            _context.FixationPoints.Add(nfp);
            await _context.SaveChangesAsync();

            FixationPoint newFixationPoint = _context.FixationPoints.OrderBy(lfp => lfp.Id).LastOrDefault(lfp => lfp.VehicleId == veh.Id);

            if (lastFixationPoint != null)
            {
                double tax = _taxComputer.Compute(lastFixationPoint, newFixationPoint);

                if (tax > 0)
                {
                    _messageSender.SendMessageAsync(veh.VehicleOwnerId, tax);
                }
            }

            return newFixationPoint;
        }
    }
}
