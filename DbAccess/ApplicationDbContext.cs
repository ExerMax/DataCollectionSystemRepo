using DbAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<FixationPoint> FixationPoints { get; set; } = null!;
        public DbSet<VehicleOwner> VehicleOwners { get; set; } = null!;
        public DbSet<FixationDevice> FixationDevices { get; set; } = null!;
        public DbSet<Road> Roads { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            VehicleOwner vo1 = new VehicleOwner
            {
                Id = 1,
                Name = "specialstring",
                Surname = "specialstring",
                Patronymic = "specialstring",
                TaxpayerIdentificationNumber = "specialstring",
                Phone = "specialstring",
                Address = "specialstring",
                Email = "specialstring",
                Document = "specialstring"
            };
            modelBuilder.Entity<VehicleOwner>().HasData(vo1);

            FixationDevice fd1 = new FixationDevice { Id = 1, Type = "Type1", Number = "PP1231", Address = "Ad1" };
            FixationDevice fd2 = new FixationDevice { Id = 2, Type = "Type1", Number = "PP1232", Address = "Ad2" };
            FixationDevice fd3 = new FixationDevice { Id = 3, Type = "Type1", Number = "PP1233", Address = "Ad3" };
            FixationDevice fd4 = new FixationDevice { Id = 4, Type = "Type2", Number = "PP1234", Address = "Ad4" };
            FixationDevice fd5 = new FixationDevice { Id = 5, Type = "Type3", Number = "PP1235", Address = "Ad5" };
            FixationDevice fd6 = new FixationDevice { Id = 6, Type = "Type3", Number = "PP1236", Address = "Ad6" };
            modelBuilder.Entity<FixationDevice>().HasData(fd1, fd2, fd3, fd4, fd5, fd6);

            modelBuilder.Entity<Road>().HasData(
                new Road { Id = 1, Value = 100, StartPointId = fd1.Id, EndPointId = fd2.Id },
                new Road { Id = 2, Value = 200, StartPointId = fd2.Id, EndPointId = fd3.Id },
                new Road { Id = 3, Value = 300, StartPointId = fd3.Id, EndPointId = fd4.Id },
                new Road { Id = 4, Value = 400, StartPointId = fd4.Id, EndPointId = fd5.Id },
                new Road { Id = 5, Value = 500, StartPointId = fd5.Id, EndPointId = fd6.Id }
            );
        }
    }
}
