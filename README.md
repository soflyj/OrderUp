# OrderUp

**OrderUp** is a multi-tenant .NET Web API designed to support bakeries (tenants), with secure user management, order processing, activity logging, and CI/CD deployment support. Built with Clean Architecture, it is modular, testable, and scalable.

---

## 📌 Requirements

- Multi-tenancy: Each tenant can have multiple users.
- Roles: Users have roles (stored as Enums).
- JWT Authentication:
  - Token expires in 15 minutes.
  - Required for all users.
- Auth APIs:
  - Register (sends email for verification).
  - Login (returns JWT token).
  - Forgot Password (sends reset link).
- Email Integration (for verification and password reset).
- CRUD for all entities (Tenant, User, etc.).
- Entity tracking:
  - Logs `CreatedAt`, `UpdatedAt`.
  - `LogEntry` table stores before/after change history.
- SQL Server database.
- Swagger API docs.
- Docker support.
- GitHub Actions CI/CD.
- Unit tests for services.

---

## 🧰 Tech Stack

| Layer         | Technology                      |
|---------------|----------------------------------|
| Backend       | ASP.NET Core 9 Web API           |
| Auth          | JWT (15 min expiry)              |
| Persistence   | EF Core 9 + SQL Server           |
| Architecture  | Clean Architecture               |
| Docs          | Swagger/OpenAPI                  |
| Unit Testing  | xUnit + Moq                      |
| CI/CD         | GitHub Actions + FTP Deploy      |
| Docker        | Docker + Docker Compose          |

---

## 🏛️ Clean Architecture

OrderUp.sln
├── OrderUp.Domain # Entities, Enums, Interfaces
├── OrderUp.Application # DTOs, Interfaces, Services, Contracts
├── OrderUp.Infrastructure # EF Core, Persistence, Email
├── OrderUp.API # Controllers, Program.cs, Middleware
├── OrderUp.Tests # Unit tests for services

Each layer depends only on the layer below it. `Domain` has no dependencies. `Application` uses `Domain`. `Infrastructure` implements interfaces from `Application`. `API` is the entry point.

---

## 🚀 How to Run the Project

### 1. Clone the Repo

```
git clone https://github.com/yourusername/orderup.git
cd orderup
```

### 2. Set Up the Database

Ensure SQL Server is running and update appsettings.json with your connection string.

Then apply migrations:

```
dotnet ef database update --project OrderUp.Infrastructure
```

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Build and Run

```
dotnet build
dotnet run --project OrderUp.API
```

Visit: https://localhost:5001/swagger

---

## 🧪 Running Tests
```
dotnet test
```

---

## 🐳 Docker
Build and Run with Docker Compose
```
docker-compose up --build
```
Stops and removes containers
```
docker-compose down
```

---

## 🛠️ CI/CD - GitHub Actions
On push to main, the workflow:

 - Builds the project.

 - Runs unit tests.

 - Publishes artifacts.

 - Uploads compiled files to an FTP server for deployment.

See .github/workflows/deploy.yml.

To make it work:

Store your secrets (e.g., FTP_HOST, FTP_USERNAME, FTP_PASSWORD) in GitHub repository secrets.

## 📬 Email Setup
SMTP settings should be configured in appsettings.json:

```
"EmailSettings": {
  "SmtpServer": "smtp.orderup.co.za",
  "Port": 587,
  "SenderName": "no-reply",
  "SenderEmail": "no-reply@orderup.co.za",
  "Username": "smtp-user",
  "Password": "smtp-password"
}
```

---


## 🔐 JWT Authentication
Auth API returns a JWT with 15-minute expiry.

Use the token in Authorization: Bearer <token> header.

Role-based authorization is handled via [Authorize(Roles = "Admin")].

---

## 📓 Swagger API Docs
Available at:
```
https://localhost:5001/swagger
```
Explore endpoints, send test requests, and view schemas.

---

## 💾 Logging
All data changes are tracked in the LogEntries table, storing:

- Entity name
- Table ID
- Before/After values
- Who made the change
- When

## 🙋 Support
Feel free to raise issues or contact the maintainer at developer@orderup.co.za