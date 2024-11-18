# TMA
Task Management App
# Project README

## Overview
This project demonstrates expertise in Angular and ASP.NET Core, featuring a user-friendly interface for managing tasks. It includes custom middleware to handle authorization and error management, ensuring secure access and robust error handling.

## Technologies Used
- **Angular**: For building dynamic web applications with optimized code.
- **ASP.NET Core**: Used for creating a RESTful API to manage tasks.
- **JSON**: task data is stored in a JSON file.
- **Dependency Injection**: Applied for managing logging and services.
- **Detailed Logging**: Logs requests, errors, and unauthorized access attempts to aid debugging and monitoring.

## Features

### Angular Application
- **Task Management**:
  - Reactive Forms for creating and editing tasks.
  - Confirmation modals for task deletion.
  - Efficient data processing through optimized filtering and observables.

### ASP.NET Core API for Task Management
- **Endpoints**:
  - `GET /Task/GetTasks`: Retrieves all tasks.
  - `GET /Task/GetTaskById`: Retrieves a specific Task by ID.
  - `POST /Task/AddTask`: Adds a new Task.
  - `PUT /Task/UpdateTask`: Updates an existing Task.
  - `DELETE /Task/DeleteTask`: Deletes a task by ID.

- **Custom Middleware**:
  - **Authorization Middleware**: Manages authorization tokens and provides detailed error responses (404, 401, 500).
  - **Global Expection Middleware**:  It intercepts unhandled exceptions that occur during the processing of HTTP requests, allowing you to log the error, format a response, and potentially redirect the user or provide a user-friendly error message.
  - 
## Package Reference

This project utilizes various NuGet packages to function properly. Here's a breakdown of the dependencies:

**API Project:**
--For Sqlite Database
* `Microsoft.EntityFrameworkCore.Sqlite`: Version 6.0.35 (Provides Entity Framework Core support for SQLite database)

--For SqlServer Database
* `Microsoft.EntityFrameworkCore.SqlServer`: Version 6.0.35 (Provides Entity Framework Core support for SQL Server database)
* `System.Data.SqlClient`: Version 4.9.0 (Provides access to SQL Server databases)
* `Microsoft.SqlServer.Server`: Version 1.0.0 (Might be used for server-side functionality with SQL Server)

* `Microsoft.Extensions.Configuration.Abstractions`: Version 8.0.0 (Provides abstractions for configuration management)
* `Newtonsoft.Json`: Version 13.0.3 (Provides JSON serialization and deserialization)


**Web Project:**

* `Microsoft.AspNetCore.SpaProxy`: Version 6.0.35 (Enables Single-Page Application (SPA) proxying)
* `Microsoft.EntityFrameworkCore.Design`: Version 6.0.35 (Used for design-time tools like migrations)
* `Serilog.AspNetCore`: Version 8.0.3 (Provides logging for ASP.NET Core applications)
* `Serilog.Sinks.File`: Version 6.0.0 (Enables logging to a file)
* `Newtonsoft.Json`: Version 13.0.3 (Provides JSON serialization and deserialization)
* `Swashbuckle.AspNetCore`: Version 6.9.0 (Provides Swagger for API documentation)
* `Swashbuckle.AspNetCore.Annotations`: Version 6.9.0 (Provides annotations for Swagger documentation)
* `System.Configuration.ConfigurationManager`: Version 8.0.1 (Used for configuration management)
* `System.Data.SqlClient`: Version 4.9.0 (Provides access to SQL Server databases)

## Setup Instructions

### Prerequisites
Ensure the following are installed:

- **Node.js** (version 16 or higher): Download from [nodejs.org](https://nodejs.org).
- **npm**: Comes bundled with Node.js. Verify installation by running:
  ```bash
  node -v
  npm -v
  ```

### Installation

1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   cd <repository-folder>
   ```

2. **Set Up Angular Client**:
   ```bash
   cd clientApp
   npm install
   ```
3. **Set Up Database:**
# Database Setup Instructions for TMA Project

This document provides step-by-step instructions to set up the database for the TMA project. You can configure the project to use either SQLite or SQL Server.

---

## Step 1: Clear Migration Folder

1. Navigate to the **Migrations** folder in the TMA.Api project.
2. If the folder exists, delete all files within it.

---

## Step 2: Update Configuration Files

### Modify `appsettings.json` and `appsettings.Development.json`

1. Open the `appsettings.json` and `appsettings.Development.json` files in the TMA.Web project.
2. Update the `ConnectionStrings` section:

   - **For SQLite Database:**
     ```json
     "ConnectionStrings": {
         "DefaultConnection": "Data Source=taskmanagementapp.db"
     }
     ```

   - **For SQL Server Database:**
     ```json
     "ConnectionStrings": {
         "DefaultConnection": "Server=YourServerName;Database=YourDatabaseName;User ID=YourUsername;Password=YourPassword;"
     }
     ```
     Example:
     ```json
     "ConnectionStrings": {
         "DefaultConnection": "Server=GOKUL;Database=Task_Db;Persist Security Info=true;User ID=sa;Password=sqlserver;TrustServerCertificate=true;Trusted_Connection=True;Connection Timeout=30;"
     }
     ```

---

## Step 3: Update Infrastructure File

### Modify `Infrastructure.cs`

1. Open the `Infrastructure.cs` file in the TMA.Web project.
2. Update the configuration:

   - **For SQLite Database:**
     ```csharp
     options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
     ```

   - **For SQL Server Database:**
     ```csharp
     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                          x => x.MigrationsHistoryTable("__EFMigrationsHistory"));
     ```

---

## Step 4: Update TaskContext File

### Modify `TaskContext.cs`

1. Open the `TaskContext.cs` file in the TMA.Api project.
2. Update the `ConnectionStrings` section:

   - **For SQLite Database:**
     ```csharp
     // optionsBuilder.UseSqlite("Data Source=taskmanagementapp.db");
     ```

   - **For SQL Server Database:**
     ```csharp
     optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
     ```

---

## Step 5: Apply Migrations and Update the Database

1. Open the **Package Manager Console** in Visual Studio.
2. Set **TMA.Web** as the Default Project.
3. Run the following commands:

   - **Command 1: Add Initial Migration**
     ```bash
     dotnet ef migrations add InitialCreate --project "D:\Task Management App 1\TMA.Api\TMA.Api.csproj" --startup-project "D:\Task Management App 1\TMA.Web\TMA.Web.csproj"
     ```

   - **Command 2: Update the Database**
     ```bash
     dotnet ef database update --project "D:\Task Management App 1\TMA.Api\TMA.Api.csproj" --startup-project "D:\Task Management App 1\TMA.Web\TMA.Web.csproj"
     
4. **Set Up ASP.NET Core API**:
   - Navigate back to the root directory:
     ```bash
     cd ..
     ```
   - Restore dependencies:
     ```bash
     dotnet restore
     ```
   - Run the API application:
     ```bash
     dotnet run
     ```
5. **Run API Unit test project:**

- Navigate to test project directory from root:

- cd TMA.Test

- Run the test project:

- dotnet test

6.**5Run Angular test specs:**

- Navigate to client app from root directory:

- cd TMA.Web\ClientApp.

- Run angular test specs:

- ng test

7.**View Swagger Documentation: Access the API documentation at:**

- https://localhost:7284/swagger/index.html or  http://localhost:5093/swagger/index.html
8.**Login Credentials:**
- Admin:   Email : "admin@example.com", Password : "Admin@123";
- Manager:   Email : "manager@example.com", Password : "Manager@123";
- User:   Email : "user@example.com", Password : "User@123";
- Role Management and Logout is Available  under NavbarMenu User Icon.(Note:Role Management can be accessable only for  Admin)

