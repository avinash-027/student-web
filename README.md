# Student Portal Web Application

## Overview

The **Student Portal Web** application is designed for managing student data with functionalities like Create, Read, Update, and Delete (CRUD) operations. Built with **ASP.NET Core MVC**, **C#**, and **Entity Framework Core (EF Core)**, this project provides a simple yet scalable platform to store and manage student details, ensuring smooth and effective database interactions.

## Technologies Used

- **Frontend:**
  - **HTML**, **CSS**, **JavaScript**: For building a responsive and interactive user interface.
  
- **Backend:**
  - **ASP.NET Core MVC**: A robust and scalable server-side framework.
  - **C#**: The primary programming language used in this project.
  - **Entity Framework Core**: An Object-Relational Mapper (ORM) used for managing database operations.

- **Database:**
  - **Microsoft SQL Server**: For efficient data storage and retrieval.

## Features

- **CRUD Operations**:
  - **Create**: Add new student records.
  - **Read**: View a list of students and their details.
  - **Update**: Edit student records.
  - **Delete**: Remove student records from the database.

- **Validation**:
  - Input validation for fields such as **Email** and **Phone** using **Data Annotations**.
  - Custom validation to ensure that the student's **Name** and **Phone** are not the same.

- **Logging**:
  - Integrated **logging** for tracking activities and debugging.

## Getting Started

### Prerequisites

- **Visual Studio** (or any compatible IDE for C# development).
- **Microsoft SQL Server** for database management.

### Setup Instructions

1. **Clone the Repository:**

    ```bash
    git clone https://github.com/avinash-027/student-web.git
    ```

2. **Set Up the Environment:**
   - Ensure **Visual Studio** is installed.
   - Open the solution file `StudentPortal.Web.sln` in Visual Studio.

3. **Configure the Database:**
   - Open `appsettings.json` and update the connection string to match your local SQL Server database configuration.

4. **Run the Application:**
   - Press **F5** or click the "Start" button in Visual Studio to build and run the application.

### Migrations (Database Setup)

1. Open the **Package Manager Console** in Visual Studio.
2. Run the following commands to apply the migrations:

    ```bash
    Add-Migration InitialCreate
    Update-Database
    ```
