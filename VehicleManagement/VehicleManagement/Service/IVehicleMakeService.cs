using VehicleManagement.Models;

namespace VehicleManagement.Service
{
    public interface IVehicleMakeService
    {
        Task<IQueryable<VehicleMake>> GetAllVehicleMakesAsync();
        Task<VehicleMake> GetVehicleMakeByIdAsync(int id);
        Task AddVehicleMakeAsync(VehicleMake vehicleMake);
        Task UpdateVehicleMakeAsync(VehicleMake vehicleMake);
        Task DeleteVehicleMakeAsync(int id);
    }
}
