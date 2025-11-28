using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ProductDAL _productDAL;
        private readonly CategoryDAL _categoryDAL;
        private readonly SupplierDAL _supplierDAL;

        public HomeController(DbConnection db)
        {
            _productDAL = new ProductDAL(db);
            _categoryDAL = new CategoryDAL(db);
            _supplierDAL = new SupplierDAL(db);
        }

        public IActionResult Index()
        {
            // Get summary statistics
            //Here we are using the DAL classes to fetch data from the database
            // and passing the counts to the ViewBag for display in the view.
            ViewBag.TotalProducts = _productDAL.GetAll().Count;
            ViewBag.TotalCategories = _categoryDAL.GetAll().Count;
            ViewBag.TotalSuppliers = _supplierDAL.GetAll().Count;
            ViewBag.TotalInventoryValue = _productDAL.GetTotalInventoryValue();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
