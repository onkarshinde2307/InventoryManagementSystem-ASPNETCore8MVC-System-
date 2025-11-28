using System.Data;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Data;

namespace InventoryManagementSystem.DAL
{
    public class SupplierDAL
    {
        private readonly DbConnection _db;

        public SupplierDAL(DbConnection db)
        {
            _db = db;
        }

        public List<Supplier> GetAll()
        {
            var list = new List<Supplier>();
            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Suppliers", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Supplier
                    {
                        SupplierID = (int)dr["SupplierID"],
                        SupplierName = dr["SupplierName"].ToString(),
                        ContactNumber = dr["ContactNumber"].ToString(),
                        Address = dr["Address"].ToString()
                    });
                }
            }
            return list;
        }

        public Supplier GetById(int id)
        {
            Supplier supplier = null;
            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Suppliers WHERE SupplierID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    supplier = new Supplier
                    {
                        SupplierID = (int)dr["SupplierID"],
                        SupplierName = dr["SupplierName"].ToString(),
                        ContactNumber = dr["ContactNumber"].ToString(),
                        Address = dr["Address"].ToString()
                    };
                }
            }
            return supplier;
        }

        public void Add(Supplier supplier)
        {
            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Suppliers (SupplierName, ContactNumber, Address) VALUES (@name, @contact, @address)", con);
                cmd.Parameters.AddWithValue("@name", supplier.SupplierName);
                cmd.Parameters.AddWithValue("@contact", supplier.ContactNumber);
                cmd.Parameters.AddWithValue("@address", supplier.Address);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Supplier supplier)
        {
            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Suppliers SET SupplierName=@name, ContactNumber=@contact, Address=@address WHERE SupplierID=@id", con);
                cmd.Parameters.AddWithValue("@id", supplier.SupplierID);
                cmd.Parameters.AddWithValue("@name", supplier.SupplierName);
                cmd.Parameters.AddWithValue("@contact", supplier.ContactNumber);
                cmd.Parameters.AddWithValue("@address", supplier.Address);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Suppliers WHERE SupplierID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
