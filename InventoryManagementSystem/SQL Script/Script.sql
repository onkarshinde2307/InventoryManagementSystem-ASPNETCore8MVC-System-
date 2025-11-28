--  Create Database
CREATE DATABASE InventoryDB;
GO

USE InventoryDB;
GO

--  Create Categories Table
CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);
GO

--   Create Suppliers Table
CREATE TABLE Suppliers (
    SupplierID INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(150) NOT NULL,
    ContactNumber NVARCHAR(50),
    Address NVARCHAR(250)
);
GO

--  Create Products Table
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(150) NOT NULL,
    CategoryID INT NOT NULL,
    SupplierID INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    QuantityInStock INT NOT NULL,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);
GO

-- Optional: Create Users Table (if using authentication)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(250) NOT NULL,
    FullName NVARCHAR(150),
    Email NVARCHAR(150)
);
GO

--  Insert Sample Categories
INSERT INTO Categories (CategoryName) VALUES
('Electronics'),
('Stationery'),
('Furniture'),
('Clothing');
GO

--  Insert Sample Suppliers
INSERT INTO Suppliers (SupplierName, ContactNumber, Address) VALUES
('Tech Supplier Inc', '1234567890', '123 Tech Street'),
('Office Supplies Co', '9876543210', '456 Office Avenue'),
('Home & Furniture Ltd', '5551234567', '789 Home Road');
GO

--  Insert Sample Products
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, QuantityInStock) VALUES
('Laptop', 1, 1, 1200.00, 10),
('Printer', 1, 2, 300.00, 15),
('Office Chair', 3, 3, 150.00, 20),
('Notebook', 2, 2, 5.50, 200),
('T-Shirt', 4, 3, 20.00, 50);
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('dbo.Users') AND type = 'U')
BEGIN
    CREATE TABLE Users (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(256) NOT NULL,
        FullName NVARCHAR(150) NULL,
        Email NVARCHAR(150) NULL
    );
END
