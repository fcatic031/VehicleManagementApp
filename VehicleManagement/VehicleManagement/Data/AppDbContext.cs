using Microsoft.EntityFrameworkCore;
using VehicleManagement.Models;

namespace VehicleManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleModel>()
        .HasOne(vm => vm.VehicleMake)
        .WithMany(v => v.VehicleModels)
        .HasForeignKey(vm => vm.VehicleMakeId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VehicleMake>().HasData(
           new VehicleMake { Id = 1, Name = "BMW", Abrv = "BMW" },
           new VehicleMake { Id = 2, Name = "Ford", Abrv = "FRD" },
           new VehicleMake { Id = 3, Name = "Volkswagen", Abrv = "VW" },
           new VehicleMake { Id = 4, Name = "Tesla", Abrv = "TSL" },
           new VehicleMake { Id = 5, Name = "Toyota", Abrv = "TYT" }
       );

            // Seeding podataka za VehicleModel (dva modela za svaki make)
            modelBuilder.Entity<VehicleModel>().HasData(
                new VehicleModel { Id = 1, VehicleMakeId = 1, Name = "X5", Abrv = "X5" },
                new VehicleModel { Id = 2, VehicleMakeId = 1, Name = "325", Abrv = "325" },

                new VehicleModel { Id = 3, VehicleMakeId = 2, Name = "Fiesta", Abrv = "FST" },
                new VehicleModel { Id = 4, VehicleMakeId = 2, Name = "Mustang", Abrv = "MST" },

                new VehicleModel { Id = 5, VehicleMakeId = 3, Name = "Golf", Abrv = "GLF" },
                new VehicleModel { Id = 6, VehicleMakeId = 3, Name = "Passat", Abrv = "PST" },

                new VehicleModel { Id = 7, VehicleMakeId = 4, Name = "Model S", Abrv = "S" },
                new VehicleModel { Id = 8, VehicleMakeId = 4, Name = "Model X", Abrv = "X" },

                new VehicleModel { Id = 9, VehicleMakeId = 5, Name = "Corolla", Abrv = "COR" },
                new VehicleModel { Id = 10, VehicleMakeId = 5, Name = "Camry", Abrv = "CMR" }
            );
        }

    }
}
