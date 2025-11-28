using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Data;

namespace InventoryManagementSystem.DAL
{
    public class CategoryDAL
    {
        private readonly DbConnection _db;

        public CategoryDAL(DbConnection db)
        {
            _db = db;
        }

        // Get All Categories
        public List<Category> GetAll()
        {
            var categories = new List<Category>();
            using (SqlConnection conn = _db.GetConnection())
            {
                string query = "SELECT CategoryID, CategoryName FROM Categories";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString()
                    });
                }
            }
            return categories;
        }

        // Get Category by ID
        public Category GetById(int id)
        {
            Category category = null;
            using (SqlConnection conn = _db.GetConnection())
            {
                string query = "SELECT CategoryID, CategoryName FROM Categories WHERE CategoryID=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    category = new Category
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString()
                    };
                }
            }
            return category;
        }

        // Add Category
        public void Add(Category category)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                string query = "INSERT INTO Categories (CategoryName) VALUES (@name)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", category.CategoryName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Update Category
        public void Update(Category category)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                string query = "UPDATE Categories SET CategoryName=@name WHERE CategoryID=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", category.CategoryName);
                cmd.Parameters.AddWithValue("@id", category.CategoryID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Delete Category
        public void Delete(int id)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                string query = "DELETE FROM Categories WHERE CategoryID=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
