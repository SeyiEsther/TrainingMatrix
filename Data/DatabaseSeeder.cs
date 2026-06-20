using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(TrainingMatrixDbContext context)
    {
        var now = DateTime.UtcNow;

        var departments = new[]
        {
            new Department { Id = 1, Name = "Engineering", Description = "Engineering and technical development", IsActive = true, CreatedDate = now },
            new Department { Id = 2, Name = "Operations", Description = "Operational and manufacturing activities", IsActive = true, CreatedDate = now },
            new Department { Id = 3, Name = "Human Resources", Description = "Employee management and development", IsActive = true, CreatedDate = now },
            new Department { Id = 4, Name = "Quality Assurance", Description = "Quality control and compliance", IsActive = true, CreatedDate = now },
            new Department { Id = 5, Name = "Software Development", Description = "Software engineering and development", ParentDepartmentId = 1, IsActive = true, CreatedDate = now },
            new Department { Id = 6, Name = "Hardware Engineering", Description = "Hardware design and development", ParentDepartmentId = 1, IsActive = true, CreatedDate = now },
            new Department { Id = 7, Name = "Manufacturing", Description = "Production and assembly", ParentDepartmentId = 2, IsActive = true, CreatedDate = now },
            new Department { Id = 8, Name = "Logistics", Description = "Supply chain and distribution", ParentDepartmentId = 2, IsActive = true, CreatedDate = now },
            new Department { Id = 9, Name = "Recruitment", Description = "Talent acquisition", ParentDepartmentId = 3, IsActive = true, CreatedDate = now },
            new Department { Id = 10, Name = "Training & Development", Description = "Employee training and skills development", ParentDepartmentId = 3, IsActive = true, CreatedDate = now },
        };

        var skills = new[]
        {
            new Skill { Id = 1, Name = "C# Programming", Category = "Technical", Description = "Object-oriented programming with C#", IsActive = true, CreatedDate = now },
            new Skill { Id = 2, Name = "SQL Database", Category = "Technical", Description = "Database design and query optimization", IsActive = true, CreatedDate = now },
            new Skill { Id = 3, Name = "ASP.NET Core", Category = "Technical", Description = "Web application development with ASP.NET Core", IsActive = true, CreatedDate = now },
            new Skill { Id = 4, Name = "Azure Cloud", Category = "Technical", Description = "Microsoft Azure cloud services", IsActive = true, CreatedDate = now },
            new Skill { Id = 5, Name = "Git Version Control", Category = "Technical", Description = "Source code management with Git", IsActive = true, CreatedDate = now },
            new Skill { Id = 6, Name = "Machine Operation", Category = "Operational", Description = "Safe operation of manufacturing machinery", IsActive = true, CreatedDate = now },
            new Skill { Id = 7, Name = "Quality Inspection", Category = "Quality", Description = "Product quality inspection procedures", IsActive = true, CreatedDate = now },
            new Skill { Id = 8, Name = "Forklift Operation", Category = "Operational", Description = "Certified forklift operation", IsActive = true, CreatedDate = now },
            new Skill { Id = 9, Name = "Health & Safety", Category = "Safety", Description = "Workplace health and safety compliance", IsActive = true, CreatedDate = now },
            new Skill { Id = 10, Name = "First Aid", Category = "Safety", Description = "Emergency first aid response", IsActive = true, CreatedDate = now },
            new Skill { Id = 11, Name = "Leadership", Category = "Soft Skills", Description = "Team leadership and management", IsActive = true, CreatedDate = now },
            new Skill { Id = 12, Name = "Communication", Category = "Soft Skills", Description = "Effective workplace communication", IsActive = true, CreatedDate = now },
            new Skill { Id = 13, Name = "Project Management", Category = "Management", Description = "Project planning and execution", IsActive = true, CreatedDate = now },
            new Skill { Id = 14, Name = "Risk Assessment", Category = "Quality", Description = "Workplace risk identification and mitigation", IsActive = true, CreatedDate = now },
            new Skill { Id = 15, Name = "Lean Manufacturing", Category = "Operational", Description = "Lean principles and waste reduction", IsActive = true, CreatedDate = now },
        };

        var courses = new[]
        {
            new TrainingCourse { Id = 1, Name = "Advanced C# Development", CourseCode = "CS-101", Category = "Technical", DurationHours = 40, ValidityMonths = 24, IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 2, Name = "SQL Server Administration", CourseCode = "SQL-201", Category = "Technical", DurationHours = 32, ValidityMonths = 24, IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 3, Name = "ASP.NET Core Web Development", CourseCode = "WEB-301", Category = "Technical", DurationHours = 48, ValidityMonths = 18, IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 4, Name = "Azure Fundamentals", CourseCode = "AZ-900", Category = "Technical", DurationHours = 24, ValidityMonths = 36, IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 6, Name = "CNC Machine Operation", CourseCode = "MFG-101", Category = "Operational", DurationHours = 16, ValidityMonths = 36, IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 8, Name = "Quality Control Inspection", CourseCode = "QA-101", Category = "Quality", DurationHours = 24, ValidityMonths = 24, IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 9, Name = "Forklift Operator Certification", CourseCode = "LOG-101", Category = "Operational", DurationHours = 8, ValidityMonths = 36, IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 10, Name = "Workplace Health & Safety", CourseCode = "SAF-101", Category = "Safety", DurationHours = 8, ValidityMonths = 12, IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 11, Name = "First Aid at Work", CourseCode = "SAF-201", Category = "Safety", DurationHours = 16, ValidityMonths = 24, IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 15, Name = "Lean Six Sigma Yellow Belt", CourseCode = "OPS-301", Category = "Operational", DurationHours = 24, ValidityMonths = 36, IsActive = true, CreatedDate = now },
        };

        var employees = new[]
        {
            new Employee { Id = 1, EmployeeNumber = "EMP001", FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", DepartmentId = 5, HireDate = new DateTime(2018, 3, 15), IsActive = true, CreatedDate = now },
            new Employee { Id = 2, EmployeeNumber = "EMP002", FirstName = "Bob", LastName = "Smith", Email = "bob.smith@example.com", DepartmentId = 5, HireDate = new DateTime(2020, 6, 1), IsActive = true, CreatedDate = now },
            new Employee { Id = 3, EmployeeNumber = "EMP003", FirstName = "Carol", LastName = "Williams", Email = "carol.williams@example.com", DepartmentId = 6, HireDate = new DateTime(2019, 9, 10), IsActive = true, CreatedDate = now },
            new Employee { Id = 4, EmployeeNumber = "EMP004", FirstName = "David", LastName = "Brown", Email = "david.brown@example.com", DepartmentId = 7, HireDate = new DateTime(2017, 1, 20), IsActive = true, CreatedDate = now },
            new Employee { Id = 5, EmployeeNumber = "EMP005", FirstName = "Emma", LastName = "Davis", Email = "emma.davis@example.com", DepartmentId = 7, HireDate = new DateTime(2021, 4, 5), IsActive = true, CreatedDate = now },
            new Employee { Id = 6, EmployeeNumber = "EMP006", FirstName = "Frank", LastName = "Miller", Email = "frank.miller@example.com", DepartmentId = 8, HireDate = new DateTime(2019, 2, 14), IsActive = true, CreatedDate = now },
            new Employee { Id = 7, EmployeeNumber = "EMP007", FirstName = "Grace", LastName = "Wilson", Email = "grace.wilson@example.com", DepartmentId = 4, HireDate = new DateTime(2016, 11, 30), IsActive = true, CreatedDate = now },
        };

        context.Departments.AddRange(departments);
        context.Skills.AddRange(skills);
        context.TrainingCourses.AddRange(courses);
        context.Employees.AddRange(employees);
        await context.SaveChangesAsync();

        departments[0].HeadOfDepartmentId = 3;
        departments[4].HeadOfDepartmentId = 1;
        departments[6].HeadOfDepartmentId = 4;
        departments[7].HeadOfDepartmentId = 6;
        departments[3].HeadOfDepartmentId = 7;
        await context.SaveChangesAsync();

        context.EmployeeSkills.AddRange(
            new EmployeeSkill { EmployeeId = 1, SkillId = 1, ProficiencyLevel = 5, AcquiredDate = new DateTime(2020, 1, 1), LastAssessedDate = new DateTime(2024, 6, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 1, SkillId = 3, ProficiencyLevel = 4, AcquiredDate = new DateTime(2021, 3, 1), LastAssessedDate = new DateTime(2024, 5, 15), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 1, SkillId = 5, ProficiencyLevel = 4, AcquiredDate = new DateTime(2019, 6, 1), LastAssessedDate = new DateTime(2024, 3, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 2, SkillId = 1, ProficiencyLevel = 3, AcquiredDate = new DateTime(2022, 1, 1), LastAssessedDate = new DateTime(2024, 4, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 2, SkillId = 2, ProficiencyLevel = 3, AcquiredDate = new DateTime(2022, 6, 1), LastAssessedDate = new DateTime(2024, 6, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 4, SkillId = 6, ProficiencyLevel = 5, AcquiredDate = new DateTime(2019, 1, 1), LastAssessedDate = new DateTime(2024, 10, 15), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 4, SkillId = 9, ProficiencyLevel = 4, AcquiredDate = new DateTime(2018, 9, 20), LastAssessedDate = new DateTime(2024, 9, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 4, SkillId = 15, ProficiencyLevel = 3, AcquiredDate = new DateTime(2023, 1, 1), LastAssessedDate = new DateTime(2024, 8, 10), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 6, SkillId = 8, ProficiencyLevel = 5, AcquiredDate = new DateTime(2019, 2, 14), LastAssessedDate = new DateTime(2024, 11, 5), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 6, SkillId = 9, ProficiencyLevel = 4, AcquiredDate = new DateTime(2019, 2, 14), LastAssessedDate = new DateTime(2024, 9, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 7, SkillId = 7, ProficiencyLevel = 5, AcquiredDate = new DateTime(2018, 1, 1), LastAssessedDate = new DateTime(2024, 12, 15), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 7, SkillId = 14, ProficiencyLevel = 4, AcquiredDate = new DateTime(2019, 1, 1), LastAssessedDate = new DateTime(2024, 11, 30), CreatedDate = now });

        context.EmployeeTrainings.AddRange(
            new EmployeeTraining { EmployeeId = 1, TrainingCourseId = 1, CompletionDate = new DateTime(2023, 6, 15), ExpiryDate = new DateTime(2025, 6, 15), Status = "Completed", Score = 95.5m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 1, TrainingCourseId = 3, CompletionDate = new DateTime(2023, 9, 20), ExpiryDate = new DateTime(2025, 3, 20), Status = "Completed", Score = 92.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 4, TrainingCourseId = 6, CompletionDate = new DateTime(2022, 5, 10), ExpiryDate = new DateTime(2025, 5, 10), Status = "Completed", Score = 94.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 4, TrainingCourseId = 10, CompletionDate = new DateTime(2024, 8, 15), ExpiryDate = new DateTime(2025, 8, 15), Status = "Completed", Score = 98.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 6, TrainingCourseId = 9, CompletionDate = new DateTime(2023, 3, 15), ExpiryDate = new DateTime(2026, 3, 15), Status = "Completed", Score = 96.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 7, TrainingCourseId = 10, CompletionDate = new DateTime(2024, 8, 15), ExpiryDate = new DateTime(2025, 8, 15), Status = "Completed", Score = 99.0m, CreatedDate = now });

        context.DepartmentSkillRequirements.AddRange(
            new DepartmentSkillRequirement { DepartmentId = 5, SkillId = 1, RequiredCount = 2, MinimumProficiencyLevel = 3, Priority = "High", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 5, SkillId = 3, RequiredCount = 2, MinimumProficiencyLevel = 3, Priority = "High", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 7, SkillId = 6, RequiredCount = 3, MinimumProficiencyLevel = 3, Priority = "Critical", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 7, SkillId = 9, RequiredCount = 2, MinimumProficiencyLevel = 3, Priority = "High", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 8, SkillId = 8, RequiredCount = 2, MinimumProficiencyLevel = 4, Priority = "Critical", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 4, SkillId = 7, RequiredCount = 2, MinimumProficiencyLevel = 4, Priority = "Critical", IsActive = true, CreatedDate = now });

        context.TrainingTasks.AddRange(
            new TrainingTask { Id = 1, Name = "New Hire Safety Induction", Description = "Complete safety orientation for new manufacturing staff", DepartmentId = 7, SortOrder = 1, TargetEmployeeCount = 10, IsActive = true, CreatedDate = now },
            new TrainingTask { Id = 2, Name = "Code Review Certification", Description = "Peer code review training for developers", DepartmentId = 5, SortOrder = 1, TargetEmployeeCount = 5, IsActive = true, CreatedDate = now });

        context.TrainingTaskSkills.AddRange(
            new TrainingTaskSkill { TrainingTaskId = 1, SkillId = 9 },
            new TrainingTaskSkill { TrainingTaskId = 2, SkillId = 1 },
            new TrainingTaskSkill { TrainingTaskId = 2, SkillId = 5 });

        context.AuditLogs.Add(new AuditLog
        {
            Timestamp = now,
            ActionType = "Seed",
            EntityType = "Database",
            EntityId = "local",
            Details = "Local SQLite database seeded with sample data.",
            PerformedBy = "System"
        });

        await context.SaveChangesAsync();
    }
}
