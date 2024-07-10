using DbAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess
{
    public class ExternalServicesDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<VehicleOwner> VehicleOwners { get; set; } = null!;
        public DbSet<TaxMessage> TaxMessages { get; set; }
        public ExternalServicesDbContext(DbContextOptions<ExternalServicesDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            VehicleOwner vo1 = new VehicleOwner
            {
                Id = 1,
                Name = "Иван",
                Surname = "Иванович",
                Patronymic = "Иванов",
                TaxpayerIdentificationNumber = "1234567890",
                Phone = "88001234567",
                Address = "Улица Пушкина, дом Колотушкина, 3",
                Email = "Ivan@email.com",
                Document = "1234555777"
            };
            VehicleOwner vo2 = new VehicleOwner
            {
                Id = 2,
                Name = "Василий",
                Surname = "Васильевич",
                Patronymic = "Васильев",
                TaxpayerIdentificationNumber = "9087654321",
                Phone = "88006543210",
                Address = "Улица Пушкина, дом Колотушкина, 4",
                Email = "Vasiliy@email.com",
                Document = "1234333444"
            };

            modelBuilder.Entity<VehicleOwner>().HasData(vo1, vo2);

            /*
            modelBuilder.Entity<Vehicle>().HasData(
                    new Vehicle { Id = 1, StateRegistrationPlate = "Tom", IsTruck = true, VehicleOwner = vo1 },
                    new Vehicle { Id = 2, StateRegistrationPlate = "Bob", IsTruck = false, VehicleOwner = vo1 },
                    new Vehicle { Id = 3, StateRegistrationPlate = "Sam", IsTruck = false, VehicleOwner = vo2 }
            );
            */
        }
    }
}
