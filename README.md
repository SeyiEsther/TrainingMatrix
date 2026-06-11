# Training Matrix

A web application for managing employee training, skills, and compliance built with ASP.NET Core 10 Razor Pages.

## What It Does

- **Departments**: Manage organisational departments and sub-departments
- **Employees**: Track employees, their departments, hire dates, and contact info
- **Skills**: Define skills with categories and descriptions
- **Training Courses**: Manage training courses with providers, duration, cost, and validity
- **Compliance Dashboard**: View how many employees in each department meet skill requirements
- **Audit Log**: Track all create/update/delete actions with timestamps

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
```

To load seed data:

```bash
sqlcmd -S <server> -d TrainingMatrix -i seeddata.sql
```

## Configuration

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<your-server>;Database=TrainingMatrix;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## Running the Application

```bash
dotnet restore
dotnet run
```

The application uses Windows Authentication. Ensure your IIS or Kestrel environment supports Negotiate authentication.

## Tech Stack

- ASP.NET Core 10 Razor Pages
- Entity Framework Core 10
- SQL Server
- Bootstrap 5
- Windows Authentication
