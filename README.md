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

3. **Set Up ASP.NET Core API**:
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
 4. **Run API Unit test project:**

- Navigate to test project directory from root:

- cd TMA.Test

- Run the test project:

- dotnet test

5.**5Run Angular test specs:**

- Navigate to client app from root directory:

- cd TMA.Web\ClientApp.

- Run angular test specs:

- ng test

6.**View Swagger Documentation: Access the API documentation at:**

- https://localhost:7284/swagger/index.html or  http://localhost:5093/swagger/index.html
7.**Login Credentials:**
- Admin:   Email : "admin@example.com", Password : "Admin@123";
- Manager:   Email : "manager@example.com", Password : "Manager@123";
- User:   Email : "user@example.com", Password : "User@123";
- Role Management and Logout is Available  under NavbarMenu User Icon.(Note:Role Management can be accessable only for  Admin)

