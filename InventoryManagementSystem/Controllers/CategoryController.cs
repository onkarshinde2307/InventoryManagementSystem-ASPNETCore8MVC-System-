using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.DAL;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly CategoryDAL _categoryDAL;

        public CategoryController(DbConnection db)
        {
            _categoryDAL = new CategoryDAL(db);
        }

        //Here we use index method to show all categories
        public IActionResult Index()
        {
            var categories = _categoryDAL.GetAll();
            return View(categories);
        }

        // here we create the new category and show the create view
        public IActionResult Create()
        {
            return View();
        }

        // here actully create the category in database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryDAL.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //A view to edit the category and show the edit view
        public IActionResult Edit(int id)
        {
            var category = _categoryDAL.GetById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // Post method to actually edit the category in database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryDAL.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // category delete view to confirm delete 
        public IActionResult Delete(int id)
        {
            var category = _categoryDAL.GetById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        //  Post method to actually delete the category from database  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryDAL.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
