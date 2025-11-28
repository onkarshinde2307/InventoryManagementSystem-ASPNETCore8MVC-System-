using System.Data;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Data;

namespace InventoryManagementSystem.DAL
{
    public class ProductDAL
    {
        private readonly DbConnection _db;

        public ProductDAL(DbConnection db)
        {
            _db = db;
        }

        // Fetch product list with Category & Supplier Names
        public List<Product> GetAll(string search = "")
        {
            List<Product> list = new List<Product>();

            using (SqlConnection con = _db.GetConnection())
            {
                string query = @"SELECT p.ProductID, p.ProductName, c.CategoryName, s.SupplierName, 
                                 p.Price, p.QuantityInStock 
                                 FROM Products p
                                 JOIN Categories c ON p.CategoryID = c.CategoryID
                                 JOIN Suppliers s ON p.SupplierID = s.SupplierID";

                if (!string.IsNullOrEmpty(search))
                    query += " WHERE p.ProductName LIKE @search";

                SqlCommand cmd = new SqlCommand(query, con);

                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Product
                    {
                        ProductID = (int)dr["ProductID"],
                        ProductName = dr["ProductName"].ToString(),
                        CategoryName = dr["CategoryName"].ToString(),
                        SupplierName = dr["SupplierName"].ToString(),
                        Price = (decimal)dr["Price"],
                        QuantityInStock = (int)dr["QuantityInStock"]
                    });
                }
            }
            return list;
        }

        // Get Product By ID
        public Product GetById(int id)
        {
            Product product = null;
            using (SqlConnection con = _db.GetConnection())
            {
                string query = "SELECT * FROM Products WHERE ProductID=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    product = new Product
                    {
                        ProductID = (int)dr["ProductID"],
                        ProductName = dr["ProductName"].ToString(),
                        CategoryID = (int)dr["CategoryID"],
                        SupplierID = (int)dr["SupplierID"],
                        Price = (decimal)dr["Price"],
                        QuantityInStock = (int)dr["QuantityInStock"]
                    };
                }
            }
            return product;
        }

        // Add Product
        public void Add(Product p)
        {
            using (SqlConnection con = _db.GetConnection())
            {
                string query = @"INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, QuantityInStock) 
                                 VALUES (@name, @category, @supplier, @price, @qty)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", p.ProductName);
                cmd.Parameters.AddWithValue("@category", p.CategoryID);
                cmd.Parameters.AddWithValue("@supplier", p.SupplierID);
                cmd.Parameters.AddWithValue("@price", p.Price);
                cmd.Parameters.AddWithValue("@qty", p.QuantityInStock);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Update Product
        public void Update(Product p)
        {
            using (SqlConnection con = _db.GetConnection())
            {
                string query = @"UPDATE Products SET ProductName=@name, CategoryID=@category, 
                                 SupplierID=@supplier, Price=@price, QuantityInStock=@qty 
                                 WHERE ProductID=@id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", p.ProductID);
                cmd.Parameters.AddWithValue("@name", p.ProductName);
                cmd.Parameters.AddWithValue("@category", p.CategoryID);
                cmd.Parameters.AddWithValue("@supplier", p.SupplierID);
                cmd.Parameters.AddWithValue("@price", p.Price);
                cmd.Parameters.AddWithValue("@qty", p.QuantityInStock);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Delete Product
        public void Delete(int id)
        {
            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ProductID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Export & Reporting Helper (DataTable)
        public DataTable GetAllProductsTable()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT p.ProductID, p.ProductName, c.CategoryName, s.SupplierName, 
                             p.Price, p.QuantityInStock,
                             (p.Price * p.QuantityInStock) AS TotalValue
                      FROM Products p
                      LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                      LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID", con);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // Calculate total inventory value
        public decimal GetTotalInventoryValue()
        {
            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT SUM(Price * QuantityInStock) FROM Products", con);

                con.Open();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
    }
}
