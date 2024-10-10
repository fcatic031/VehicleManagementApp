using Microsoft.EntityFrameworkCore;
using VehicleManagement.Data;
using VehicleManagement.Models;

namespace VehicleManagement.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly AppDbContext _db;
        public VehicleMakeService(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddVehicleMakeAsync(VehicleMake vehicleMake)
        {
            _db.Add(vehicleMake);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteVehicleMakeAsync(int id)
        {
            var make = await _db.VehicleMakes.FindAsync(id);
            if (make != null)
            {
                _db.VehicleMakes.Remove(make);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<VehicleMake>> GetAllVehicleMakesAsync()
        {
            return await _db.VehicleMakes.ToListAsync();
        }

        public async Task<VehicleMake> GetVehicleMakeByIdAsync(int id)
        {
            return await _db.VehicleMakes.FindAsync(id);
        }

        public async Task UpdateVehicleMakeAsync(VehicleMake vehicleMake)
        {
            _db.VehicleMakes.Update(vehicleMake);
            await _db.SaveChangesAsync();
        }
    }
}
