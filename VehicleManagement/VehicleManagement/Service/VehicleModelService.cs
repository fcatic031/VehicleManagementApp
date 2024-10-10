using Microsoft.EntityFrameworkCore;
using VehicleManagement.Data;
using VehicleManagement.Models;

namespace VehicleManagement.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly AppDbContext _db;

        public VehicleModelService(AppDbContext db) { _db = db; }

        public async Task AddVehicleModelAsync(VehicleModel vehicleModel)
        {
            _db.VehicleModels.Add(vehicleModel);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteVehicleModelAsync(int id)
        {
            var model = await _db.VehicleModels.FindAsync(id);
            if (model != null)
            {
                _db.VehicleModels.Remove(model);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<VehicleModel>> GetAllVehicleModelAsync()
        {
            return await _db.VehicleModels.ToListAsync();
        }

        public async Task<VehicleModel> GetVehicleModelByIdAsync(int id)
        {
            return await _db.VehicleModels.FindAsync(id);
        }

        public async Task UpdateVehicleModelAsync(VehicleModel vehicleModel)
        {
            _db.VehicleModels.Update(vehicleModel);
            await _db.SaveChangesAsync();
        }
    }
}
