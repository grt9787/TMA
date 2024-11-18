# Project README

## Overview

This project includes various components that demonstrate proficiency in Angular, ASP.NET Core. The main features include a user-friendly interface for managing Task.The project includes custom middleware to handle authorization and exception management, ensuring secure access and providing detailed error handling.

## Technologies Used

- **Angular**: For building dynamic web applications with a focus on optimizing code.
- **ASP.NET Core**: For creating a RESTful API to manage Task.
- **JSON**: For storing Task data in a file.
- **Dependency Injection**: Utilized for logging and service management.
- **Detailed Logging**: Logs request details, errors, and unauthorized access attempts for better debugging and monitoring.

## Features

### Angular Application

- **Task Management**:
  - Modals for creating/editing Task.
  - Confirmation modal for Task deletion.
  - Optimized filtering mechanisms and observable handling for efficient data processing.

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
* 
### ASP.NET Core API for Task Management


- **Task Management Endpoints**:
  - **POST /auth /login**:Login into Application.
  - **GET /admin /GetRoles**: Retrieves a list of all Roles.
  - **GET /admin /GetActions**: Retrieves a list of all Action.
  - **GET /admin /GetRoleActions**: Retrieves a list of all RoleAction.
  - **POST /admin/UpdateRoleActions**: Updates an existing updateRoleAction Details.
  - **GET /Tasks /GetTasks**: Retrieves a list of all Task.
  - **GET /Tasks/GetTaskById**: Retrieves a specific Task by ID.
  - **POST /Tasks/AddTasks**: Adds a new Task to the list.
  - **PUT /Tasks/UpdateTask**: Updates an existing Task's details.
  - **DELETE /Tasks/DeleteTask**: Deletes a Task by ID.

- **Custom Error Handling Middleware**:
  - **CookieAuthMiddleware**: Custom middleware that handles authorization token processing and provides detailed error responses for various status codes (404, 401, 500).

## Setup Instructions

### Prerequisites

Before you begin, ensure you have the following installed:

- **Node.js**: Version 16 or higher. You can download it from [nodejs.org](https://nodejs.org/).
- **npm**: This comes bundled with Node.js. Verify your installation by running:
  ```bash
  node -v
  npm -v

**Installation**
**1.Clone the Repository:**

git clone <repository-url>
cd <repository-folder>

DefaultConnection

**2.Set Up Angular Client:**

Open:
cd clientApp.

Run
npm install.

**3.Set Up Database:**
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
     ```

---




**4.Set Up ASP.NET Core API:**

 - Navigate back to the root directory:

- cd ..

- Restore the dependencies:

- dotnet restore

-  Run the API application:

- dotnet run
**5.Login Credentials:**
- Admin:   Email : "admin@example.com", Password : "Admin@123";
- Manager:   Email : "manager@example.com", Password : "Manager@123";
- User:   Email : "user@example.com", Password : "User@123";
- Role Management and Logout is Available  under NavbarMenu User Icon.(Note:Role Management can be accessable only for  Admin)



