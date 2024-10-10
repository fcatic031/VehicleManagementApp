using System.Threading.Tasks;
using VehicleManagement.Models;

namespace VehicleManagement.Service
{
    public interface IVehicleModelService
    {
        Task<List<VehicleModel>> GetAllVehicleModelAsync();
        Task<VehicleModel> GetVehicleModelByIdAsync(int id);
        Task AddVehicleModelAsync(VehicleModel vehicleModel);
        Task UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task DeleteVehicleModelAsync(int id);
    }
}
