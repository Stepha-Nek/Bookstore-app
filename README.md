#  Bookstore API

A full-stack bookstore application built with ASP.NET Core, React, and TypeScript.

## 🔗 Live Demo
- **Frontend:** https://bookstore-6gjg6zqa0-stepha-neks-projects.vercel.app
- **Backend API:** https://helpful-flexibility-production-5263.up.railway.app/api/books

##  Tech Stack

### Backend
- **ASP.NET Core 10** — REST API
- **Entity Framework Core** — ORM
- **SQLite** — Database
- **JWT Authentication** — Secure endpoints
- **ASP.NET Identity** — User management

### Frontend
- **React 18** — UI framework
- **TypeScript** — Type safety
- **Axios** — HTTP client
- **React Router** — Navigation

### DevOps
- **Docker** — Containerisation
- **GitHub Actions** — CI/CD pipeline
- **Railway** — Backend hosting
- **Vercel** — Frontend hosting

##  Features
- Browse all books
- Register and login with JWT authentication
- Add, update and delete books (authenticated users only)
- Fully tested with xUnit (unit, integration and controller tests)
- Automated CI/CD — tests run on every push

##  Running Locally

### Prerequisites
- .NET 10 SDK
- Node.js
- Docker (optional)

### Backend
```bash
cd firstAPI
dotnet restore
dotnet run
```

### Frontend
```bash
cd frontend
npm install
npm start
```

### Docker
```bash
docker-compose up --build
```

## 🧪 Running Tests
```bash
cd firstAPI.Tests
dotnet test
```

## 📡 API Endpoints

### Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/auth/register | Register a new user |
| POST | /api/auth/login | Login and get JWT token |

### Books
| Method | Endpoint | Auth Required | Description |
|--------|----------|--------------|-------------|
| GET | /api/books | ❌ | Get all books |
| GET | /api/books/{id} | ❌ | Get book by ID |
| POST | /api/books | ✅ | Add a book |
| PUT | /api/books/{id} | ✅ | Update a book |
| DELETE | /api/books/{id} | ✅ | Delete a book |

## 🔐 Environment Variables

Copy `appsettings.example.json` to `appsettings.json` and fill in:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY",
    "Issuer": "FirstAPI",
    "Audience": "FirstAPIUsers"
  }
}
