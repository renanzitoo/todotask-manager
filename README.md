# Scheduling API Project

A simple **Task Scheduling REST API** developed in **C# with ASP.NET Core and Entity Framework Core**,  
focused on practicing **RESTful APIs**, **clean architecture**, **async/await**, and **CRUD operations**.

The project allows creating, updating, deleting, and querying tasks by **ID**, **date**, **status**, and **title**.

------------------------------------------------------------------------

## 👩🏾‍💻 Technologies

- [x] C#
- [x] .NET 8
- [x] ASP.NET Core Web API
- [x] Entity Framework Core
- [x] LINQ
- [x] Git
- [x] GitHub
- [x] JetBrains Rider / Visual Studio / VS Code

------------------------------------------------------------------------

## 💻 Requirements

Before starting, make sure you have:

- .NET SDK 8 or higher
- Git
- An IDE (Rider, Visual Studio, or VS Code)
- A configured database provider (SQL Server, MySQL, SQLite, etc.)

------------------------------------------------------------------------

## 🚀 Running the project

Clone the repository:

```bash
git clone <repository-url>
```

Access the project folder:

```bash
cd SchedulingAPI
```

Restore dependencies:

```bash
dotnet restore
```

Apply database migrations (if configured):

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```

The API will be available at:

```text
https://localhost:5001
http://localhost:5000
```

------------------------------------------------------------------------

## 📌 API Endpoints

### Create a task
**POST** `/api/tasks`

```json
{
  "title": "Study ASP.NET",
  "description": "Practice Web API with EF Core",
  "status": 0
}
```

---

### Get all tasks
**GET** `/api/tasks`

---

### Get task by ID
**GET** `/api/tasks/{id}`

---

### Update a task
**PUT** `/api/tasks/{id}`

```json
{
  "id": 1,
  "title": "Study ASP.NET Core",
  "description": "CRUD + Filters",
  "dueDate": "2026-01-11T10:00:00",
  "status": 1
}
```

---

### Delete a task
**DELETE** `/api/tasks/{id}`

---

### Get tasks by due date
**GET** `/api/tasks/by-date?dueDate=2026-01-11`

---

### Get tasks by status
**GET** `/api/tasks/by-status?status=1`

---

### Get tasks by title
**GET** `/api/tasks/by-title?title=Study ASP.NET`

------------------------------------------------------------------------

## 📁 Project Structure

```text
SchedulingAPI.sln
├── SchedulingAPI
│   ├── Controllers
│   │   └── TodoTaskController.cs
│   ├── Context
│   │   └── TodoTaskContext.cs
│   ├── Entities
│   │   ├── TodoTask.cs
│   │   └── TaskStatus.cs
│   └── Program.cs
```

------------------------------------------------------------------------

## 🧠 Key Concepts Practiced

- RESTful API design
- Async programming with async/await
- Entity Framework Core with DbContext
- Filtering data with LINQ
- Proper HTTP status codes

------------------------------------------------------------------------

## 🤝 Author

**Renan Costa**  
GitHub: https://github.com/renanzitoo
