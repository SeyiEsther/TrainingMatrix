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
- SQL Server (local or remote)

## Setup

### Option A: Restore database backup

Restore `TrainingMatrixDb.bak` to your SQL Server instance using SQL Server Management Studio (SSMS) or:

```bash
sqlcmd -S <server> -Q "RESTORE DATABASE TrainingMatrix FROM DISK='<path>\TrainingMatrixDb.bak'"
```

### Option B: Run the SQL script

```bash
sqlcmd -S <server> -i script.sql
sqlcmd -S <server> -d TrainingMatrix -i Migrations/20250619_AddAuditLogs.sql
```

To load seed data:

```bash
sqlcmd -S <server> -d TrainingMatrix -i seeddata.sql
```

## Configuration

**Do not commit production credentials.** Set the connection string via one of:

1. **User Secrets** (recommended for local dev):

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=<your-server>;Database=TrainingMatrix;Trusted_Connection=True;TrustServerCertificate=True;"
```

2. **Environment variable**:

```bash
export ConnectionStrings__DefaultConnection="Server=<your-server>;Database=TrainingMatrix;Trusted_Connection=True;TrustServerCertificate=True;"
```

3. **Local override file** (`appsettings.Local.json`, git-ignored):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<your-server>;Database=TrainingMatrix;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

The committed `appsettings.json` contains a localhost placeholder only.

## Running the Application

```bash
dotnet restore TrainingSkillsApp.csproj
dotnet run --project TrainingSkillsApp.csproj
```

Browse to `https://localhost:5001` (see `Properties/launchSettings.json`).

The application uses Windows Authentication (Negotiate). Ensure your IIS or Kestrel environment supports Negotiate authentication. On Linux/macOS during development, authentication may require additional configuration.

## Project Structure

| Path | Purpose |
|------|---------|
| `Pages/` | Razor Pages UI (Departments, Employees, Skills, Courses, Compliance) |
| `Services/` | Business logic (compliance, audit, file storage, transfers) |
| `Models/` | Entity Framework domain models |
| `Data/` | DbContext and database configuration |
| `Migrations/` | EF migrations and supplemental SQL scripts |
| `script.sql` | Full database schema |
| `seeddata.sql` | Sample data including compliance requirements |

## Tech Stack

- ASP.NET Core 10 Razor Pages
- Entity Framework Core 10
- SQL Server
- Bootstrap 5
- Windows Authentication

## Security Notes

- Connection strings and secrets must not be committed to source control
- Uploaded files are stored outside `wwwroot` in `App_Data/uploads/training` with type and size validation
- All pages require authentication via the default authorization policy
