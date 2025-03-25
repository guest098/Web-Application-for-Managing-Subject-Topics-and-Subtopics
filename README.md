# Web-Application-for-Managing-Subject-Topics-and-Subtopics

This project is a web application that allows users to manage subjects and subtopics using ASP.NET Core (Backend) and JavaScript (Frontend).

📂 Backend (ASP.NET Core Web API)
🔹 Technologies Used
Language: C#

Framework: ASP.NET Core Web API

Database: SQL Server

ORM: Entity Framework Core

Development Tools: Visual Studio

📌 Features
Manage Subjects: Create, Read, Update, Delete (CRUD)

Manage Subtopics: CRUD operations for subtopics related to subjects

RESTful API: API endpoints to interact with the frontend

Database Integration: Uses SQL Server for storing subjects and subtopics

CORS Support: Allows the frontend to communicate with the backend

⚙️ Setup Instructions
Clone the repository

bash
Copy
git clone https://github.com/your-username/subject-management.git
cd subject-management/Backend
Configure the database (Update appsettings.json with your database connection string)

Run database migrations

bash
Copy
dotnet ef database update
Run the backend server

bash
Copy
dotnet run
API will be available at
https://localhost:7148/api/subjects
https://localhost:7148/api/subtopics

📂 Frontend (ASP.NET MVC & JavaScript)
🔹 Technologies Used
Frontend Framework: ASP.NET Core MVC

Language: JavaScript, jQuery

Styling: CSS, Bootstrap

Development Tools: Visual Studio Code

📌 Features
Displays Subjects and Subtopics in a single-page interface

CRUD Operations for Subjects and Subtopics

User-friendly UI with animations and modern design

AJAX API Requests to interact with the backend without page reloads

