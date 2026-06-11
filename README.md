# Training Matrix

An ASP.NET Core 10 Razor Pages application for managing employee training, skills, and compliance.

## Features

- **Department Management** – Hierarchical departments with sub-departments and heads of department
- **Employee Management** – Track employees, their department assignments, and transfer history
- **Skills Catalogue** – Define skills with categories and proficiency levels
- **Training Courses** – Manage training course catalogue with validity and scoring
- **Compliance Dashboard** – View per-department skill compliance against defined requirements
- **Audit Logging** – Track key changes across the system

## Technology Stack

- ASP.NET Core 10 Razor Pages
- Entity Framework Core 10 with SQL Server
- Bootstrap 5
- Windows Authentication

## Setup

### Prerequisites

- .NET 10 SDK
- SQL Server (2019 or later recommended)

### Database Setup

**Option A – Restore from backup:**

```
RESTORE DATABASE TrainingMatrix
FROM DISK = 'path\to\backup.bak'
WITH MOVE 'TrainingMatrix' TO 'C:\Data\TrainingMatrix.mdf',
     MOVE 'TrainingMatrix_log' TO 'C:\Data\TrainingMatrix_log.ldf'
```

**Option B – Run migrations:**

```bash
dotnet ef database update
```

### Connection String

Update the connection string in `appsettings.json` to point to your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TrainingMatrix;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER` with your SQL Server instance name (e.g. `localhost`, `.\SQLEXPRESS`, or a named instance).

## Running the Application

```bash
# Restore NuGet packages
dotnet restore

# Run the application
dotnet run
```

The application will be available at `https://localhost:5001` (or the port shown in the console output).

## Notes

- The application uses **Windows Authentication**. Ensure IIS or Kestrel is configured for Windows Auth in your environment.
- For development, you may need to enable Windows Authentication in `launchSettings.json`.
- File uploads are stored under `wwwroot/uploads/training/`.
