# 📦 Inventory Management System  
### 🚀 ASP.NET Core 8 MVC | ADO.NET / EF Core | SQL Server  

The **Inventory Management System** is a **web-based application 🌐** built using **ASP.NET Core 8 MVC**, designed to efficiently manage **Products 🛒, Categories 🗂️, Suppliers 🚚**, and track **Inventory Levels 📊**.  

It follows the **MVC architecture 🏗️**, uses **ADO.NET / EF Core 🗄️** for database connectivity, and is built with **clean coding practices ✨** to ensure **scalability 🚀, security 🔒, and high performance ⚡**.

---

## 📋 Table of Contents  
- 🚀 Project Overview  
- 🛠️ Tech Stack  
- 🔥 Features  
- 🗄️ Database Schema  
- ⚙️ Installation & Database Setup  
- 📁 Folder Structure  
- 📤 Push Project to GitHub  
- 📌 Future Enhancements  
- 📝 License  

---

## 🚀 Project Overview  
This system helps businesses and organizations manage and track inventory efficiently. Users can **perform CRUD operations ✏️🗑️**, **search 🔍**, **filter ⚡**, and validate forms ✔️ for **Products, Categories, and Suppliers**.  

Ideal for **retail stores, warehouses, and e-commerce back-office systems**.

---

## 🛠️ Tech Stack  

| Technology | Description |
|------------|-------------|
| ASP.NET Core 8 MVC | Backend Framework 🌐 |
| C# | Programming Language 💻 |
| ADO.NET / EF Core | Database Access 🗄️ |
| SQL Server | Database 🗃️ |
| Bootstrap 5 | Frontend Styling 🎨 |
| Visual Studio 2022 | IDE 🖥️ |
| LINQ | Querying Data 🔎 |

---

## 🔥 Key Features  

### 👤 User Management *(Optional)*  
✔ User Login & Registration 🔑  

### 📂 Category Management  
✔ Create, Edit, Delete, View Categories 🗂️  

### 🚚 Supplier Management  
✔ Manage Supplier Details (Name, Contact, Address) 🏭  

### 🛒 Product Management  
✔ Full CRUD operations ✏️🗑️  
✔ Track Price 💲 and Stock Quantity 📦  
✔ Assign Category & Supplier 🏷️  

### 🔍 Search & Filter  
✔ Search by Product Name, Category, Supplier 🔎  
✔ Filter product lists ⚡  

### ⚙️ Others  
✔ Responsive UI with Bootstrap 🎨  
✔ Server-side Validation ✔️  
✔ Exception Handling ⚠️  

---

## 🗄️ Database Schema  

**Categories**
| Field | Type |
|-------|------|
| CategoryID (PK) | INT |
| CategoryName | NVARCHAR(100) |

**Suppliers**
| Field | Type |
|-------|------|
| SupplierID (PK) | INT |
| SupplierName | NVARCHAR(100) |
| ContactNumber | NVARCHAR(50) |
| Address | NVARCHAR(250) |

**Products**
| Field | Type |
|-------|------|
| ProductID (PK) | INT |
| ProductName | NVARCHAR(150) |
| CategoryID (FK) | INT |
| SupplierID (FK) | INT |
| Price | DECIMAL(18,2) |
| QuantityInStock | INT |

---

## ⚙️ Installation & Database Setup  

### 📥 Prerequisites
- Visual Studio 2022 🖥️  
- .NET SDK 8 🔧  
- SQL Server / SQL Express 🗄️  
- SQL Server Management Studio (SSMS) 🛠️  
- Git 🐙  

### 📂 Clone the Repository
```bash
git clone https://github.com/onkarshinde2307/InventoryManagementSystem-ASPNETCore8MVC-System-.git
cd "D:\.NET Course\Learning MVC\MVC 8 Dot Net Core\InventoryManagementSystem-ASPNETCore8MVC"
💾 Database Setup

Create Database

CREATE DATABASE InventoryDB;


Run SQL Scripts
Use the files in /DatabaseScripts folder to create tables and seed sample data.

Update Connection String
In appsettings.json, update your server name:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=InventoryDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

▶️ Run the Application

Visual Studio: Open .sln, restore NuGet packages, build, and press F5

CLI:

dotnet restore
dotnet build
dotnet run

📁 Folder Structure
InventoryManagementSystem-ASPNETCore8MVC/
│── Controllers/
│── Models/
│── Views/
│── DataAccess/
│── wwwroot/
│── DatabaseScripts/
│── appsettings.json
│── InventoryManagementSystem.csproj
│── README.md

📤 Push Project to GitHub
git init
git add .
git commit -m "Initial commit - Inventory Management System using ASP.NET Core 8 MVC"
git remote add origin https://github.com/onkarshinde2307/InventoryManagementSystem-ASPNETCore8MVC-System-.git
git branch -M main
git push -u origin main

📌 Future Enhancements

🔹 Role-Based Authentication (Admin/User)
🔹 Export to Excel / PDF
🔹 Pagination & Sorting
🔹 Dashboard & Charts 📊
🔹 API Integration 🌐
🔹 Docker / Cloud Deployment ☁️

📝 License

This project is licensed under the MIT License.

⭐ If you find this project useful, please give it a star on GitHub! ⭐


---

This **all-in-one README** now covers everything from **project description, tech stack, features, database, installation, run instructions, folder structure, Git push steps, and future enhancements**, all in a **single file**.  

If you want, I can also **create a ready-to-use `.gitignore`** for ASP.NET Core so your GitHub repo doesn’t include `bin/obj` folders — making it fully production-ready.  

Do you want me to do that next?
