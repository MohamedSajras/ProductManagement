# 🛍️ Product Management API

An ASP.NET Core Web API built to manage products with full CRUD functionality, stock operations, and distributed-safe 6-digit unique ID generation. This project follows clean coding standards, uses Entity Framework Core with code-first migrations, and includes unit tests for key scenarios.

---

## 🚀 Features

- ✅ RESTful endpoints for product CRUD operations
- 🔐 Auto-generated 6-digit unique Product IDs (safe for distributed environments)
- 📦 Stock management endpoints (increment/decrement)
- 🧱 EF Core Code-First database setup
- 🧪 Unit tests for core logic
- 🧼 Clean architecture and best practices
-    Swagger documentation

---

## 📦 API Endpoints

| Method | Endpoint                                 | Description                          |
|--------|------------------------------------------|--------------------------------------|
| POST   | `/api/products`                          | Create a new product                 |
| GET    | `/api/products`                          | Get all products                     |
| GET    | `/api/products/{id}`                     | Get a product by ID                  |
| PUT    | `/api/products/{id}`                     | Update a product                     |
| DELETE | `/api/products/{id}`                     | Delete a product                     |
| PUT    | `/api/products/decrement-stock/{id}/{q}` | Decrease stock by quantity           |
| PUT    | `/api/products/add-to-stock/{id}/{q}`    | Increase stock by quantity           |

---

## 🧠 Product Model

Each product includes:

- `Id`: Auto-generated 6-digit unique identifier
- `Name`: Product name
- `Description`: Optional description
- `Price`: Decimal value
- `StockQuantity`: Current stock
- `Category`: Optional category
- `Brand`: Optional brand
- `CreatedAt` / `UpdatedAt`: Timestamps
- `IsActive`: Boolean flag

---

## 🛠️ Tech Stack

- ASP.NET Core 8.0
- Entity Framework Core
- xUnit for testing
- SQL Server (or SQLite for local dev)
- Swagger (OpenAPI) for documentation

---

## 🧪 Running Locally

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server or SQLite
- Visual Studio 2022+ or VS Code

### Setup

```bash
git clone https://github.com/your-username/product-api.git
cd product-api
dotnet restore
dotnet ef database update
dotnet run
