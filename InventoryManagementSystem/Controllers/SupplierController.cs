using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly SupplierDAL _supplierDAL;

        //  Constructor to initialize SupplierDAL
        public SupplierController(DbConnection db)
        {
            _supplierDAL = new SupplierDAL(db);
        }


        //list of all suppliers in the index view
        public IActionResult Index()
        {
            return View(_supplierDAL.GetAll());
        }

        // Create new supplier - GET and POST
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _supplierDAL.Add(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // Edit existing supplier - GET and POST
        public IActionResult Edit(int id)
        {
            var supplier = _supplierDAL.GetById(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            _supplierDAL.Update(supplier);
            return RedirectToAction(nameof(Index));
        }

        //  delete the supplier if you do not have simply delete 
        //we have 2 delete and deleteconfimed 
        public IActionResult Delete(int id)
        {
            var supplier = _supplierDAL.GetById(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _supplierDAL.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
