using DbAccess;
using DbAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionSystem.Services
{
    public class DbWriter : IDbWriter
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbWriter> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public DbWriter(ApplicationDbContext context, ILogger<DbWriter> logger, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public bool WriteToDb(VehicleOwner vo, Vehicle veh)
        {
            if (vo == null || veh == null)
            {
                return false;
            }

            VehicleOwner? o = _context.VehicleOwners.FirstOrDefault(owner => owner.TaxpayerIdentificationNumber == vo.TaxpayerIdentificationNumber);

            if (veh.IsTruck)
            {
                if (o == null)
                {
                    _logger.LogInformation("Db does not have this VO");

                    VehicleOwner newVo = new VehicleOwner
                    {
                        Address = vo.Address,
                        Name = vo.Name,
                        Surname = vo.Surname,
                        Patronymic = vo.Patronymic,
                        TaxpayerIdentificationNumber = vo.TaxpayerIdentificationNumber,
                        Phone = vo.Phone,
                        Email = vo.Email,
                        Document = vo.Document
                    };

                    _context.VehicleOwners.Add(newVo);
                    _context.SaveChanges();
                }
                else
                {
                    _logger.LogInformation("Db does have this VO");
                }

                o = _context.VehicleOwners.FirstOrDefault(owner => owner.TaxpayerIdentificationNumber == vo.TaxpayerIdentificationNumber);

                if (vo != null)
                {
                    Vehicle newVeh = new Vehicle
                    {
                        IsTruck = veh.IsTruck,
                        StateRegistrationPlate = veh.StateRegistrationPlate,
                        VehicleOwner = o
                    };
                    _context.Vehicles.Add(newVeh);
                    _context.SaveChanges();
                }
            }
            else
            {
                Vehicle newVeh = new Vehicle
                {
                    IsTruck = false,
                    StateRegistrationPlate = veh.StateRegistrationPlate,
                    VehicleOwner = _context.VehicleOwners.FirstOrDefault(owner => owner.Id == 1)
                };
                _context.Vehicles.Add(newVeh);
                _context.SaveChanges();
            }

            return true;
        }
    }
}
