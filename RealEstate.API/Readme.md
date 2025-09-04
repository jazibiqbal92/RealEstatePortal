# 🏠 Real Estate Management System

A full-stack real estate listing and favorites application built with **ASP.NET Core**, **Entity Framework Core**, and **React**.  
This README includes **everything** needed for initial setup — database, migrations, seeding.

---

## 📦 Project Structure

RealEstate.API/ → ASP.NET Core Web API (Startup Project, Data Seeding)
RealEstate.Application/ → Application layer (DTOs, Services)
RealEstate.Domain/ → Domain models and Enums
RealEstate.Infrastructure/ → EF Core DbContext, Repositories



## 🛠 Prerequisites

Before starting, ensure you have:

- **.NET 8 SDK** 
- **SQL Server** (or compatible)
- **EF Core CLI Tools** installed globally:

## 🛠 Migrations

If migrations already exist:

dotnet ef migrations add InitialCreate --project RealEstate.Infrastructure --startup-project RealEstate.API
dotnet ef database update --project RealEstate.Infrastructure --startup-project RealEstate.API

- ## 🛠 Connection Strings
Edit RealEstate.API/appsettings.json and place your server info

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=RealEstateDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}

- ## Seeding

We have 20 sample properties in JSON format for quick setup and testing.

RealEstate.Infrastructure/Data/SeedData/properties.json

- ## CORS

After setting the front react application, update CORS url and point it to react app 

  "Cors": {
    "AllowedOrigins": [
      "http://localhost:3000"
    ]