namespace InventoryManagementSystem.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

        // For displaying related data
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
    }
}
