# Student Portal Web Application

## Overview

The **Student Portal Web** application is designed for managing student data with functionalities like Create, Read, Update, and Delete (CRUD) operations. Built using **ASP.NET Core MVC**, **C#**, and **Entity Framework Core (EF Core)**, this project offers a simple yet scalable platform for storing and managing student details, ensuring smooth and effective database interactions.

## Technologies Used

- **Frontend:**
  - **HTML**, **CSS**, **JavaScript**: For building a responsive, user-friendly interface.
  
- **Backend:**
  - **ASP.NET Core MVC**: A powerful and scalable framework for server-side development.
  - **C#**: The primary programming language for the application.
  - **Entity Framework Core**: An ORM (Object-Relational Mapper) used for handling database operations.

- **Database:**
  - **Microsoft SQL Server**: Used for efficient data storage and management.

## Features

- **CRUD Operations**:
  - **Create**: Add new student records to the system.
  - **Read**: View a list of students with their detailed information.
  - **Update**: Edit existing student records.
  - **Delete**: Remove student records from the database.

- **Input Validation**:
  - Fields like **Email** and **Phone** are validated using **Data Annotations**.
  - Custom validation ensures that **Name** and **Phone** fields are not identical.

- **Logging**:
  - Integrated **logging** functionality for tracking activities, errors, and debugging purposes.

## Getting Started

### Prerequisites

- **Visual Studio** (or any compatible IDE for C# development).
- **Microsoft SQL Server** for database management.

### Setup Instructions

1. **Clone the Repository:**

    ```bash
    git clone https://github.com/avinash-027/student-web.git
    ```

2. **Set Up the Development Environment:**
   - Install **Visual Studio** if it's not already installed.
   - Open the solution file `StudentPortal.Web.sln` in Visual Studio.

3. **Configure the Database:**
   - Open `appsettings.json` and update the connection string to match your local SQL Server setup.

4. **Run the Application:**
   - Press **F5** or click the "Start" button in Visual Studio to build and launch the application.

### Migrations (Database Setup)

1. Open the **Package Manager Console** in Visual Studio.
2. Run the following commands to apply database migrations:

    ```bash
    Add-Migration InitialCreate
    Update-Database
    ```

### Notes

- Ensure the **SQL Server** instance is running.
- Set up the logger path in `Program.cs` to enable proper logging functionality.
