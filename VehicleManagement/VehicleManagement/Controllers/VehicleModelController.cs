using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Models;
using VehicleManagement.Service;

namespace VehicleManagement.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleModelService _service;
        private readonly IVehicleMakeService _serviceMakes;

        public VehicleModelController(IVehicleModelService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var vehicleModels = await _service.GetAllVehicleModelAsync();
            return View(vehicleModels);
        }


        public async Task<IActionResult> Create()
        {
            var vehicleMakes = await _serviceMakes.GetAllVehicleMakesAsync();
          
            ViewBag.VehicleMakes = new SelectList(vehicleMakes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModel vehicleModel)
        {


            if (ModelState.IsValid)
            {
                await _service.AddVehicleModelAsync(vehicleModel);
                return RedirectToAction("Index");
            }

            ViewBag.VehicleMakes = new SelectList(await _serviceMakes.GetAllVehicleMakesAsync(), "Id", "Name");
            return View(vehicleModel);


        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var vehicleModel = await _service.GetVehicleModelByIdAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            return View(vehicleModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateVehicleModelAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleModel);

        }

        public async Task<IActionResult> Delete(int id)
        {
            var vehicleModel = await _service.GetVehicleModelByIdAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            return View(vehicleModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteVehicleModelAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
