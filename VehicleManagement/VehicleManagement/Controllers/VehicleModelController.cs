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

        public VehicleModelController(IVehicleModelService service, IVehicleMakeService serviceMakes)
        {
            _service = service;
            _serviceMakes = serviceMakes;
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

                Console.WriteLine("Entering Create POST method");
                Console.WriteLine($"Vehicle Name: {vehicleModel.Name}");
                Console.WriteLine($"Vehicle Abrv: {vehicleModel.Abrv}");
                Console.WriteLine($"VehicleMakeId: {vehicleModel.VehicleMakeId}");

                
                if (vehicleModel.VehicleMakeId == 0)
                {
                    Console.WriteLine("VehicleMakeId is not set properly.");
                }

                await _service.AddVehicleModelAsync(vehicleModel);
                return RedirectToAction("Index");
            }
            Console.WriteLine("ModelState is not valid");

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
            var vehicleMakes = await _serviceMakes.GetAllVehicleMakesAsync();
            ViewBag.VehicleMakes = new SelectList(vehicleMakes, "Id", "Name");
            return View(vehicleModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateVehicleModelAsync(vehicleModel);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.VehicleMakes = new SelectList(await _serviceMakes.GetAllVehicleMakesAsync());
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
