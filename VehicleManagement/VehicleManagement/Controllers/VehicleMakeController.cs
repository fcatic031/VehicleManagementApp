using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Models;
using VehicleManagement.Service;
using VehicleManagement.Util;

namespace VehicleManagement.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeService _service;


        public VehicleMakeController(IVehicleMakeService service)
        {
            _service = service;
        }

        
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";

            
            if (searchString!=null)
            {
                pageNumber = 1;
                
            } else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var vehicleMakes = _service.GetAllVehicleMakesAsync().Result.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                
                vehicleMakes = vehicleMakes.Where(v => v.Name.Contains(searchString) || v.Abrv.Contains(searchString));
            }
            vehicleMakes = sortOrder switch
            {
                "name_desc" => vehicleMakes.OrderByDescending(v => v.Name),
                "Abrv" => vehicleMakes.OrderBy(m => m.Abrv),
                "abrv_desc" => vehicleMakes.OrderByDescending(v => v.Abrv),
                _ => vehicleMakes.OrderBy(m => m.Name),
            };

            int pageSize = 3;

        
            return View(await PaginatedList<VehicleMake>.CreateAsync(vehicleMakes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Create()
        {
            var vehicleMakes = await _service.GetAllVehicleMakesAsync();
            ViewBag.VehicleMakes = new SelectList(vehicleMakes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }

            }
            if (ModelState.IsValid)
            {
                await _service.AddVehicleMakeAsync(vehicleMake);
                return RedirectToAction("Index");
            }


            return View(vehicleMake);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vehicleMake = await _service.GetVehicleMakeByIdAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateVehicleMakeAsync(vehicleMake);
                return RedirectToAction("Index");
            }
            return View(vehicleMake);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var vehicleMake = await _service.GetVehicleMakeByIdAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            try
            {
                var vehicleMake = await _service.GetVehicleMakeByIdAsync(id);
                if (vehicleMake != null)
                {
                    await _service.DeleteVehicleMakeAsync(id);
                    return RedirectToAction("Index");
                }

                return NotFound();
            }
            catch (DbUpdateException ex)
            {


                TempData["ErrorMessage"] = "Unable to delete VehicleMake. There are related Models in the database.";


                return RedirectToAction("Index");
            }
        }



    }
}
