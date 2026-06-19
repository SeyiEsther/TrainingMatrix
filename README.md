# Training Matrix

A web application for managing employee training, skills, and compliance built with ASP.NET Core 10 Razor Pages.

## What It Does

- **Departments**: Manage organisational departments and sub-departments, including skill requirements
- **Employees**: Track employees, their departments, hire dates, and contact info
- **Skills**: Define skills with categories and descriptions
- **Training Courses**: Manage training courses with providers, duration, cost, and validity
- **Training Tasks**: View department training tasks and linked skills
- **Compliance Dashboard**: View how many employees in each department meet skill requirements
- **Audit Log**: Track create/update/delete actions with timestamps

## Prerequisites

- .NET 10 SDK
- No database server required — uses **SQLite** locally

## Quick Start

```bash
dotnet restore TrainingSkillsApp.csproj
dotnet run --project TrainingSkillsApp.csproj
```

On first run the app will:

1. Create `App_Data/TrainingMatrix.db` (SQLite file)
2. Apply EF Core migrations automatically
3. Seed sample departments, employees, skills, and compliance data

Browse to `https://localhost:5001` (see `Properties/launchSettings.json`).

## Local Database

The default connection string stores the database as a single file:

```
Data Source=App_Data/TrainingMatrix.db
```

This is configured in `appsettings.json` and `appsettings.Development.json`. No SQL Server, Docker, or external server is needed.

### Reset / rebuild the database

**Option A — delete the file and restart:**

```bash
rm -f App_Data/TrainingMatrix.db App_Data/TrainingMatrix.db-*
dotnet run --project TrainingSkillsApp.csproj
```

**Option B — recreate on startup** (set in `appsettings.Development.json`):

```json
{
  "Database": {
    "RecreateOnStartup": true
  }
}
```

Set back to `false` after the next run, or the database will be wiped every startup.

### EF Core migrations

To add schema changes after editing models:

```bash
dotnet ef migrations add <MigrationName> --project TrainingSkillsApp.csproj
dotnet ef database update --project TrainingSkillsApp.csproj
```

Migrations are applied automatically on app startup via `DbInitializer`.

## Configuration

Override the database path with User Secrets or environment variables:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=App_Data/TrainingMatrix.db"
```

Or:

```bash
export ConnectionStrings__DefaultConnection="Data Source=App_Data/TrainingMatrix.db"
```

## Authentication

- **Windows (IIS/Kestrel on Windows)**: Uses Negotiate (Windows Authentication); all pages require sign-in
- **Linux/macOS local dev**: Auth is skipped so you can run without Active Directory

## Legacy SQL Server Scripts

The `script.sql`, `seeddata.sql`, and `TrainingMatrixDb.bak` files are from the original on-premises SQL Server deployment. They are **not required** for local development with SQLite.

## Project Structure

| Path | Purpose |
|------|---------|
| `Pages/` | Razor Pages UI |
| `Services/` | Business logic (compliance, audit, file storage) |
| `Models/` | Entity Framework domain models |
| `Data/` | DbContext, migrations, seeder, initializer |
| `App_Data/` | Local SQLite database (git-ignored) |
| `Migrations/` | EF Core migrations |

## Tech Stack

- ASP.NET Core 10 Razor Pages
- Entity Framework Core 10 + SQLite
- Bootstrap 5
- Windows Authentication (production on Windows only)
