using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(TrainingMatrixDbContext context)
    {
        var now = DateTime.UtcNow;

        // Three operating segments: regional logistics hubs, food processing, e-commerce
        var departments = new[]
        {
            new Department { Id = 1, Name = "Midlands Regional Hub", Description = "Regional logistics hub — inbound, storage, and outbound distribution", IsActive = true, CreatedDate = now },
            new Department { Id = 2, Name = "Riverside Food Processing", Description = "Mid-sized food processing plant — production and packaging", IsActive = true, CreatedDate = now },
            new Department { Id = 3, Name = "SwiftCart E-commerce", Description = "Growing e-commerce operation — fulfillment and returns", IsActive = true, CreatedDate = now },
            new Department { Id = 4, Name = "Warehouse Operations", Description = "Receiving, put-away, and stock control", ParentDepartmentId = 1, IsActive = true, CreatedDate = now },
            new Department { Id = 5, Name = "Transport & Dispatch", Description = "Loading bays, route planning, and outbound shipping", ParentDepartmentId = 1, IsActive = true, CreatedDate = now },
            new Department { Id = 6, Name = "Production Line", Description = "Food preparation, processing, and packaging lines", ParentDepartmentId = 2, IsActive = true, CreatedDate = now },
            new Department { Id = 7, Name = "Quality & Food Safety", Description = "HACCP, allergen control, and batch release", ParentDepartmentId = 2, IsActive = true, CreatedDate = now },
            new Department { Id = 8, Name = "Sanitation & Hygiene", Description = "Clean-in-place, hygiene audits, and pest control coordination", ParentDepartmentId = 2, IsActive = true, CreatedDate = now },
            new Department { Id = 9, Name = "Fulfillment Centre", Description = "Pick, pack, and ship customer orders", ParentDepartmentId = 3, IsActive = true, CreatedDate = now },
            new Department { Id = 10, Name = "Returns & Customer Ops", Description = "Returns processing, refunds, and service recovery", ParentDepartmentId = 3, IsActive = true, CreatedDate = now },
        };

        var skills = new[]
        {
            new Skill { Id = 1, Name = "Forklift Operation", Category = "Logistics", Description = "Counterbalance and reach truck operation in warehouse environments", IsActive = true, CreatedDate = now },
            new Skill { Id = 2, Name = "Inventory Management (WMS)", Category = "Logistics", Description = "Warehouse management system transactions and cycle counts", IsActive = true, CreatedDate = now },
            new Skill { Id = 3, Name = "Cold Chain Handling", Category = "Logistics", Description = "Temperature-controlled storage and dispatch procedures", IsActive = true, CreatedDate = now },
            new Skill { Id = 4, Name = "Commercial Driving", Category = "Logistics", Description = "HGV/LGV yard shunting and off-site delivery where licensed", IsActive = true, CreatedDate = now },
            new Skill { Id = 5, Name = "HACCP", Category = "Food Safety", Description = "Hazard analysis and critical control point monitoring", IsActive = true, CreatedDate = now },
            new Skill { Id = 6, Name = "Allergen Control", Category = "Food Safety", Description = "Segregation, labelling, and changeover between allergen profiles", IsActive = true, CreatedDate = now },
            new Skill { Id = 7, Name = "Food Hygiene", Category = "Food Safety", Description = "Personal hygiene and clean-as-you-go standards on production lines", IsActive = true, CreatedDate = now },
            new Skill { Id = 8, Name = "Production Machine Operation", Category = "Food Processing", Description = "Filling, sealing, and labelling equipment operation", IsActive = true, CreatedDate = now },
            new Skill { Id = 9, Name = "Sanitation Procedures", Category = "Food Processing", Description = "CIP/SIP routines and hygiene verification sign-off", IsActive = true, CreatedDate = now },
            new Skill { Id = 10, Name = "Pick & Pack", Category = "E-commerce", Description = "Single- and multi-item order picking with accuracy targets", IsActive = true, CreatedDate = now },
            new Skill { Id = 11, Name = "Order Management Systems", Category = "E-commerce", Description = "OMS/WMS workflows for order release and exception handling", IsActive = true, CreatedDate = now },
            new Skill { Id = 12, Name = "Returns Processing", Category = "E-commerce", Description = "Goods-in returns, grading, and restock or disposal routing", IsActive = true, CreatedDate = now },
            new Skill { Id = 13, Name = "Manual Handling", Category = "Safety", Description = "Safe lifting and load movement techniques", IsActive = true, CreatedDate = now },
            new Skill { Id = 14, Name = "Health & Safety", Category = "Safety", Description = "Site safety rules, PPE, and incident reporting", IsActive = true, CreatedDate = now },
            new Skill { Id = 15, Name = "Quality Inspection", Category = "Quality", Description = "Visual and sample-based quality checks against specifications", IsActive = true, CreatedDate = now },
        };

        var courses = new[]
        {
            new TrainingCourse { Id = 1, Name = "Forklift Operator Certification", CourseCode = "LOG-FLT-01", Category = "Logistics", DurationHours = 8, ValidityMonths = 36, Provider = "RTITB", IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 2, Name = "WMS Fundamentals", CourseCode = "LOG-WMS-01", Category = "Logistics", DurationHours = 16, ValidityMonths = 24, Provider = "In-house", IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 3, Name = "Cold Chain Awareness", CourseCode = "LOG-COLD-01", Category = "Logistics", DurationHours = 4, ValidityMonths = 12, Provider = "Cold Chain Federation", IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 4, Name = "HACCP Level 2", CourseCode = "FOOD-HACCP-02", Category = "Food Safety", DurationHours = 7, ValidityMonths = 36, Provider = "CIEH", IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 5, Name = "Level 2 Food Hygiene", CourseCode = "FOOD-HYG-02", Category = "Food Safety", DurationHours = 6, ValidityMonths = 36, Provider = "Highfield", IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 6, Name = "Allergen Awareness", CourseCode = "FOOD-ALG-01", Category = "Food Safety", DurationHours = 3, ValidityMonths = 24, Provider = "In-house", IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 7, Name = "Production Line Induction", CourseCode = "FOOD-PROD-01", Category = "Food Processing", DurationHours = 12, ValidityMonths = 24, Provider = "In-house", IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 8, Name = "E-commerce Fulfillment Basics", CourseCode = "ECOM-FF-01", Category = "E-commerce", DurationHours = 8, ValidityMonths = 18, Provider = "In-house", IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 9, Name = "Peak Season Operations", CourseCode = "ECOM-PEAK-01", Category = "E-commerce", DurationHours = 4, ValidityMonths = 12, Provider = "In-house", IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 10, Name = "Manual Handling", CourseCode = "SAF-MH-01", Category = "Safety", DurationHours = 4, ValidityMonths = 24, Provider = "IOSH", IsActive = true, CreatedDate = now },
            new TrainingCourse { Id = 11, Name = "Workplace Health & Safety", CourseCode = "SAF-WHS-01", Category = "Safety", DurationHours = 8, ValidityMonths = 12, Provider = "In-house", IsActive = true, CreatedDate = now },
        };

        var employees = new[]
        {
            // Midlands Regional Hub
            new Employee { Id = 1, EmployeeNumber = "LOG001", FirstName = "James", LastName = "Okonkwo", Email = "j.okonkwo@example.com", DepartmentId = 4, Shift = 1, HireDate = new DateTime(2019, 4, 12), IsActive = true, CreatedDate = now },
            new Employee { Id = 2, EmployeeNumber = "LOG002", FirstName = "Sarah", LastName = "Mitchell", Email = "s.mitchell@example.com", DepartmentId = 4, Shift = 2, HireDate = new DateTime(2021, 8, 3), IsActive = true, CreatedDate = now },
            new Employee { Id = 3, EmployeeNumber = "LOG003", FirstName = "Tom", LastName = "Brennan", Email = "t.brennan@example.com", DepartmentId = 5, Shift = 1, HireDate = new DateTime(2018, 11, 20), IsActive = true, CreatedDate = now },
            new Employee { Id = 4, EmployeeNumber = "LOG004", FirstName = "Priya", LastName = "Sharma", Email = "p.sharma@example.com", DepartmentId = 5, Shift = 2, HireDate = new DateTime(2022, 2, 14), IsActive = true, CreatedDate = now },
            // Riverside Food Processing
            new Employee { Id = 5, EmployeeNumber = "FOOD001", FirstName = "Maria", LastName = "Santos", Email = "m.santos@example.com", DepartmentId = 6, Shift = 1, HireDate = new DateTime(2017, 6, 1), IsActive = true, CreatedDate = now },
            new Employee { Id = 6, EmployeeNumber = "FOOD002", FirstName = "Daniel", LastName = "Kowalski", Email = "d.kowalski@example.com", DepartmentId = 6, Shift = 2, HireDate = new DateTime(2020, 9, 15), IsActive = true, CreatedDate = now },
            new Employee { Id = 7, EmployeeNumber = "FOOD003", FirstName = "Helen", LastName = "Fraser", Email = "h.fraser@example.com", DepartmentId = 7, Shift = 1, HireDate = new DateTime(2016, 3, 22), IsActive = true, CreatedDate = now },
            new Employee { Id = 8, EmployeeNumber = "FOOD004", FirstName = "Ahmed", LastName = "Hassan", Email = "a.hassan@example.com", DepartmentId = 8, Shift = 1, HireDate = new DateTime(2019, 1, 8), IsActive = true, CreatedDate = now },
            // SwiftCart E-commerce
            new Employee { Id = 9, EmployeeNumber = "ECOM001", FirstName = "Chloe", LastName = "Nguyen", Email = "c.nguyen@example.com", DepartmentId = 9, Shift = 1, HireDate = new DateTime(2023, 5, 10), IsActive = true, CreatedDate = now },
            new Employee { Id = 10, EmployeeNumber = "ECOM002", FirstName = "Ryan", LastName = "O'Brien", Email = "r.obrien@example.com", DepartmentId = 9, Shift = 2, HireDate = new DateTime(2023, 11, 1), IsActive = true, CreatedDate = now },
            new Employee { Id = 11, EmployeeNumber = "ECOM003", FirstName = "Fatima", LastName = "Ali", Email = "f.ali@example.com", DepartmentId = 10, Shift = 1, HireDate = new DateTime(2024, 2, 19), IsActive = true, CreatedDate = now },
            new Employee { Id = 12, EmployeeNumber = "ECOM004", FirstName = "Luke", LastName = "Patel", Email = "l.patel@example.com", DepartmentId = 9, Shift = 3, HireDate = new DateTime(2024, 9, 3), IsActive = true, CreatedDate = now },
        };

        context.Departments.AddRange(departments);
        context.Skills.AddRange(skills);
        context.TrainingCourses.AddRange(courses);
        context.Employees.AddRange(employees);
        await context.SaveChangesAsync();

        departments[0].HeadOfDepartmentId = 3;
        departments[1].HeadOfDepartmentId = 7;
        departments[2].HeadOfDepartmentId = 9;
        departments[3].HeadOfDepartmentId = 1;
        departments[5].HeadOfDepartmentId = 5;
        departments[8].HeadOfDepartmentId = 9;
        await context.SaveChangesAsync();

        context.EmployeeSkills.AddRange(
            // Logistics hub
            new EmployeeSkill { EmployeeId = 1, SkillId = 1, ProficiencyLevel = 5, AcquiredDate = new DateTime(2019, 5, 1), LastAssessedDate = new DateTime(2025, 4, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 1, SkillId = 2, ProficiencyLevel = 4, AcquiredDate = new DateTime(2020, 1, 1), LastAssessedDate = new DateTime(2025, 3, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 1, SkillId = 14, ProficiencyLevel = 4, AcquiredDate = new DateTime(2019, 4, 15), LastAssessedDate = new DateTime(2025, 1, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 2, SkillId = 2, ProficiencyLevel = 3, AcquiredDate = new DateTime(2021, 9, 1), LastAssessedDate = new DateTime(2025, 2, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 2, SkillId = 13, ProficiencyLevel = 3, AcquiredDate = new DateTime(2021, 8, 5), LastAssessedDate = new DateTime(2024, 8, 5), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 3, SkillId = 1, ProficiencyLevel = 4, AcquiredDate = new DateTime(2018, 12, 1), LastAssessedDate = new DateTime(2025, 5, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 3, SkillId = 4, ProficiencyLevel = 5, AcquiredDate = new DateTime(2019, 3, 1), LastAssessedDate = new DateTime(2025, 3, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 4, SkillId = 3, ProficiencyLevel = 3, AcquiredDate = new DateTime(2022, 3, 1), LastAssessedDate = new DateTime(2025, 1, 15), CreatedDate = now },
            // Food processing
            new EmployeeSkill { EmployeeId = 5, SkillId = 8, ProficiencyLevel = 5, AcquiredDate = new DateTime(2017, 7, 1), LastAssessedDate = new DateTime(2025, 6, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 5, SkillId = 7, ProficiencyLevel = 5, AcquiredDate = new DateTime(2017, 6, 15), LastAssessedDate = new DateTime(2025, 6, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 6, SkillId = 8, ProficiencyLevel = 3, AcquiredDate = new DateTime(2020, 10, 1), LastAssessedDate = new DateTime(2025, 4, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 7, SkillId = 5, ProficiencyLevel = 5, AcquiredDate = new DateTime(2016, 4, 1), LastAssessedDate = new DateTime(2025, 5, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 7, SkillId = 6, ProficiencyLevel = 5, AcquiredDate = new DateTime(2017, 1, 1), LastAssessedDate = new DateTime(2025, 5, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 7, SkillId = 15, ProficiencyLevel = 5, AcquiredDate = new DateTime(2016, 5, 1), LastAssessedDate = new DateTime(2025, 6, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 8, SkillId = 9, ProficiencyLevel = 4, AcquiredDate = new DateTime(2019, 2, 1), LastAssessedDate = new DateTime(2025, 3, 1), CreatedDate = now },
            // E-commerce
            new EmployeeSkill { EmployeeId = 9, SkillId = 10, ProficiencyLevel = 4, AcquiredDate = new DateTime(2023, 6, 1), LastAssessedDate = new DateTime(2025, 5, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 9, SkillId = 11, ProficiencyLevel = 3, AcquiredDate = new DateTime(2023, 6, 1), LastAssessedDate = new DateTime(2025, 4, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 10, SkillId = 10, ProficiencyLevel = 3, AcquiredDate = new DateTime(2023, 11, 15), LastAssessedDate = new DateTime(2025, 3, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 11, SkillId = 12, ProficiencyLevel = 3, AcquiredDate = new DateTime(2024, 3, 1), LastAssessedDate = new DateTime(2025, 2, 1), CreatedDate = now },
            new EmployeeSkill { EmployeeId = 12, SkillId = 10, ProficiencyLevel = 2, AcquiredDate = new DateTime(2024, 9, 10), LastAssessedDate = new DateTime(2025, 1, 1), CreatedDate = now });

        context.EmployeeTrainings.AddRange(
            new EmployeeTraining { EmployeeId = 1, TrainingCourseId = 1, CompletionDate = new DateTime(2023, 4, 10), ExpiryDate = new DateTime(2026, 4, 10), Status = "Completed", Score = 96.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 1, TrainingCourseId = 2, CompletionDate = new DateTime(2024, 1, 15), ExpiryDate = new DateTime(2026, 1, 15), Status = "Completed", Score = 91.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 5, TrainingCourseId = 5, CompletionDate = new DateTime(2022, 6, 1), ExpiryDate = new DateTime(2025, 6, 1), Status = "Completed", Score = 98.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 5, TrainingCourseId = 7, CompletionDate = new DateTime(2017, 7, 1), ExpiryDate = new DateTime(2025, 7, 1), Status = "Completed", Score = 94.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 7, TrainingCourseId = 4, CompletionDate = new DateTime(2023, 3, 20), ExpiryDate = new DateTime(2026, 3, 20), Status = "Completed", Score = 97.5m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 7, TrainingCourseId = 6, CompletionDate = new DateTime(2024, 2, 1), ExpiryDate = new DateTime(2026, 2, 1), Status = "Completed", Score = 95.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 9, TrainingCourseId = 8, CompletionDate = new DateTime(2023, 5, 20), ExpiryDate = new DateTime(2025, 5, 20), Status = "Completed", Score = 92.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 10, TrainingCourseId = 9, CompletionDate = new DateTime(2024, 11, 1), ExpiryDate = new DateTime(2025, 11, 1), Status = "Completed", Score = 88.0m, CreatedDate = now },
            new EmployeeTraining { EmployeeId = 3, TrainingCourseId = 11, CompletionDate = new DateTime(2024, 8, 1), ExpiryDate = new DateTime(2025, 8, 1), Status = "Completed", Score = 99.0m, CreatedDate = now });

        context.DepartmentSkillRequirements.AddRange(
            // Regional logistics hub
            new DepartmentSkillRequirement { DepartmentId = 4, SkillId = 1, RequiredCount = 2, MinimumProficiencyLevel = 4, Priority = "Critical", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 4, SkillId = 2, RequiredCount = 2, MinimumProficiencyLevel = 3, Priority = "High", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 5, SkillId = 4, RequiredCount = 1, MinimumProficiencyLevel = 4, Priority = "High", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 4, SkillId = 14, RequiredCount = 2, MinimumProficiencyLevel = 3, Priority = "High", IsActive = true, CreatedDate = now },
            // Food processing plant
            new DepartmentSkillRequirement { DepartmentId = 6, SkillId = 5, RequiredCount = 2, MinimumProficiencyLevel = 4, Priority = "Critical", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 6, SkillId = 7, RequiredCount = 3, MinimumProficiencyLevel = 3, Priority = "Critical", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 7, SkillId = 6, RequiredCount = 1, MinimumProficiencyLevel = 4, Priority = "Critical", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 8, SkillId = 9, RequiredCount = 2, MinimumProficiencyLevel = 3, Priority = "High", IsActive = true, CreatedDate = now },
            // E-commerce operation
            new DepartmentSkillRequirement { DepartmentId = 9, SkillId = 10, RequiredCount = 3, MinimumProficiencyLevel = 3, Priority = "Critical", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 9, SkillId = 11, RequiredCount = 2, MinimumProficiencyLevel = 3, Priority = "High", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 10, SkillId = 12, RequiredCount = 1, MinimumProficiencyLevel = 3, Priority = "Medium", IsActive = true, CreatedDate = now },
            new DepartmentSkillRequirement { DepartmentId = 9, SkillId = 13, RequiredCount = 3, MinimumProficiencyLevel = 2, Priority = "High", IsActive = true, CreatedDate = now });

        context.TrainingTasks.AddRange(
            new TrainingTask { Id = 1, Name = "Cold Chain Induction", Description = "Temperature monitoring and quarantine procedures for chilled goods", DepartmentId = 4, SortOrder = 1, TargetEmployeeCount = 8, IsActive = true, CreatedDate = now },
            new TrainingTask { Id = 2, Name = "Forklift Refresher", Description = "Annual practical reassessment for warehouse FLT operators", DepartmentId = 4, SortOrder = 2, TargetEmployeeCount = 4, IsActive = true, CreatedDate = now },
            new TrainingTask { Id = 3, Name = "HACCP Annual Recertification", Description = "Refresher on CCP monitoring and corrective actions", DepartmentId = 7, SortOrder = 1, TargetEmployeeCount = 6, IsActive = true, CreatedDate = now },
            new TrainingTask { Id = 4, Name = "Allergen Changeover Drill", Description = "Line clearance and verification between allergen profiles", DepartmentId = 6, SortOrder = 1, TargetEmployeeCount = 12, IsActive = true, CreatedDate = now },
            new TrainingTask { Id = 5, Name = "Peak Season Fulfillment Briefing", Description = "SLA targets, pick paths, and overflow staffing for peak trading", DepartmentId = 9, SortOrder = 1, TargetEmployeeCount = 20, IsActive = true, CreatedDate = now },
            new TrainingTask { Id = 6, Name = "Returns Grading Standard", Description = "A/B/C grading and restock rules for returned merchandise", DepartmentId = 10, SortOrder = 1, TargetEmployeeCount = 6, IsActive = true, CreatedDate = now });

        context.TrainingTaskSkills.AddRange(
            new TrainingTaskSkill { TrainingTaskId = 1, SkillId = 3 },
            new TrainingTaskSkill { TrainingTaskId = 1, SkillId = 14 },
            new TrainingTaskSkill { TrainingTaskId = 2, SkillId = 1 },
            new TrainingTaskSkill { TrainingTaskId = 3, SkillId = 5 },
            new TrainingTaskSkill { TrainingTaskId = 4, SkillId = 6 },
            new TrainingTaskSkill { TrainingTaskId = 4, SkillId = 7 },
            new TrainingTaskSkill { TrainingTaskId = 5, SkillId = 10 },
            new TrainingTaskSkill { TrainingTaskId = 5, SkillId = 11 },
            new TrainingTaskSkill { TrainingTaskId = 6, SkillId = 12 });

        context.AuditLogs.Add(new AuditLog
        {
            Timestamp = now,
            ActionType = "Seed",
            EntityType = "Database",
            EntityId = "local",
            Details = "Seeded sample data for regional logistics hubs, food processing, and e-commerce operations.",
            PerformedBy = "System"
        });

        await context.SaveChangesAsync();
    }
}
