using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.DAL;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using System.Data;
using System.Text;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductDAL _productDAL;
        private readonly CategoryDAL _categoryDAL;
        private readonly SupplierDAL _supplierDAL;

        //constructor to initialize DALs
        public ProductController(DbConnection db)
        {
            _productDAL = new ProductDAL(db);
            _categoryDAL = new CategoryDAL(db);
            _supplierDAL = new SupplierDAL(db);
        }

    
        public IActionResult Index(string search)
        {
            var products = _productDAL.GetAll(search);
            ViewBag.Search = search;
            ViewBag.TotalInventoryValue = _productDAL.GetTotalInventoryValue();
            return View(products);
        }

        // we use create for both get and post
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryDAL.GetAll();
            ViewBag.Suppliers = _supplierDAL.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _productDAL.Add(product);
            return RedirectToAction(nameof(Index));
        }

        //Same for Edit we use get and post
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _categoryDAL.GetAll();
            ViewBag.Suppliers = _supplierDAL.GetAll();
            return View(_productDAL.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _productDAL.Update(product);
            return RedirectToAction(nameof(Index));
        }

        //use delete for get and post
        public IActionResult Delete(int id)
        {
            return View(_productDAL.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productDAL.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // Export EXCEL ---But we need to install EPPlus nuget package for this to work
        //here i am getting error "License context must be set before performing non-interactive operations."
        public IActionResult ExportExcel()
        {
            DataTable dt = _productDAL.GetAllProductsTable();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Products");
                worksheet.Cells["A1"].LoadFromDataTable(dt, true);

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string fileName = $"Products_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName);
            }
        }


        //export csv file automatically 
        public IActionResult ExportCSV()
        {
            DataTable dt = _productDAL.GetAllProductsTable();
            StringBuilder sb = new StringBuilder();

            // Header
            foreach (DataColumn col in dt.Columns)
                sb.Append(col.ColumnName + ",");
            sb.AppendLine();

            // Rows
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                    sb.Append(item.ToString() + ",");
                sb.AppendLine();
            }

            string fileName = $"Products_{DateTime.Now:yyyyMMddHHmmss}.csv";
            return File(Encoding.UTF8.GetBytes(sb.ToString()),
                        "text/csv",
                        fileName);
        }
    }
}
