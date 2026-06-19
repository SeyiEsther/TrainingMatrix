-- Created by GitHub Copilot in SSMS - review carefully before executing

-- Seed Departments (Parent departments first, then child departments)
-- Note: No 'Code' column in your schema
SET IDENTITY_INSERT [dbo].[Departments] ON;

INSERT INTO [dbo].[Departments] (Id, Name, Description, ParentDepartmentId, IsActive, CreatedDate)
VALUES 
    (1, 'Engineering', 'Engineering and technical development', NULL, 1, GETUTCDATE()),
    (2, 'Operations', 'Operational and manufacturing activities', NULL, 1, GETUTCDATE()),
    (3, 'Human Resources', 'Employee management and development', NULL, 1, GETUTCDATE()),
    (4, 'Quality Assurance', 'Quality control and compliance', NULL, 1, GETUTCDATE()),
    (5, 'Software Development', 'Software engineering and development', 1, 1, GETUTCDATE()),
    (6, 'Hardware Engineering', 'Hardware design and development', 1, 1, GETUTCDATE()),
    (7, 'Manufacturing', 'Production and assembly', 2, 1, GETUTCDATE()),
    (8, 'Logistics', 'Supply chain and distribution', 2, 1, GETUTCDATE()),
    (9, 'Recruitment', 'Talent acquisition', 3, 1, GETUTCDATE()),
    (10, 'Training & Development', 'Employee training and skills development', 3, 1, GETUTCDATE());

SET IDENTITY_INSERT [dbo].[Departments] OFF;

-- Seed Skills with various categories
SET IDENTITY_INSERT [dbo].[Skills] ON;

INSERT INTO [dbo].[Skills] (Id, Name, Category, Description, IsActive, CreatedDate)
VALUES 
    (1, 'C# Programming', 'Technical', 'Object-oriented programming with C#', 1, GETUTCDATE()),
    (2, 'SQL Database', 'Technical', 'Database design and query optimization', 1, GETUTCDATE()),
    (3, 'ASP.NET Core', 'Technical', 'Web application development with ASP.NET Core', 1, GETUTCDATE()),
    (4, 'Azure Cloud', 'Technical', 'Microsoft Azure cloud services', 1, GETUTCDATE()),
    (5, 'Git Version Control', 'Technical', 'Source code management with Git', 1, GETUTCDATE()),
    (6, 'Machine Operation', 'Operational', 'Safe operation of manufacturing machinery', 1, GETUTCDATE()),
    (7, 'Quality Inspection', 'Quality', 'Product quality inspection procedures', 1, GETUTCDATE()),
    (8, 'Forklift Operation', 'Operational', 'Certified forklift operation', 1, GETUTCDATE()),
    (9, 'Health & Safety', 'Safety', 'Workplace health and safety compliance', 1, GETUTCDATE()),
    (10, 'First Aid', 'Safety', 'Emergency first aid response', 1, GETUTCDATE()),
    (11, 'Leadership', 'Soft Skills', 'Team leadership and management', 1, GETUTCDATE()),
    (12, 'Communication', 'Soft Skills', 'Effective workplace communication', 1, GETUTCDATE()),
    (13, 'Project Management', 'Management', 'Project planning and execution', 1, GETUTCDATE()),
    (14, 'Risk Assessment', 'Quality', 'Workplace risk identification and mitigation', 1, GETUTCDATE()),
    (15, 'Lean Manufacturing', 'Operational', 'Lean principles and waste reduction', 1, GETUTCDATE());

SET IDENTITY_INSERT [dbo].[Skills] OFF;

-- Seed Training Courses
-- Note: Your schema uses CourseCode instead of Code
SET IDENTITY_INSERT [dbo].[TrainingCourses] ON;

INSERT INTO [dbo].[TrainingCourses] (Id, Name, CourseCode, Description, DurationHours, ValidityMonths, Category, IsActive, CreatedDate)
VALUES 
    (1, 'Advanced C# Development', 'CS-ADV-001', 'Advanced C# programming techniques and best practices', 16, 24, 'Technical', 1, GETUTCDATE()),
    (2, 'SQL Server Fundamentals', 'SQL-FUN-001', 'Introduction to SQL Server database management', 8, 12, 'Technical', 1, GETUTCDATE()),
    (3, 'ASP.NET Core Web Development', 'ASP-WEB-001', 'Building modern web applications with ASP.NET Core', 24, 18, 'Technical', 1, GETUTCDATE()),
    (4, 'Azure Cloud Essentials', 'AZ-ESS-001', 'Microsoft Azure fundamentals and core services', 12, 24, 'Technical', 1, GETUTCDATE()),
    (5, 'Git and GitHub Workflow', 'GIT-WF-001', 'Version control best practices with Git', 4, NULL, 'Technical', 1, GETUTCDATE()),
    (6, 'CNC Machine Operation Level 1', 'CNC-L1-001', 'Basic CNC machine operation and safety', 40, 36, 'Operational', 1, GETUTCDATE()),
    (7, 'CNC Machine Operation Level 2', 'CNC-L2-001', 'Advanced CNC programming and troubleshooting', 40, 36, 'Operational', 1, GETUTCDATE()),
    (8, 'Quality Control Inspector Certification', 'QC-CERT-001', 'Quality inspection methods and standards', 16, 12, 'Quality', 1, GETUTCDATE()),
    (9, 'Forklift Operator Certification', 'FKL-CERT-001', 'Forklift safety and operation certification', 8, 36, 'Safety', 1, GETUTCDATE()),
    (10, 'Workplace Health & Safety', 'WHS-001', 'Occupational health and safety regulations', 6, 12, 'Safety', 1, GETUTCDATE()),
    (11, 'First Aid & CPR', 'FA-CPR-001', 'Emergency first aid and CPR certification', 8, 24, 'Safety', 1, GETUTCDATE()),
    (12, 'Leadership Fundamentals', 'LEAD-FUN-001', 'Essential leadership and team management skills', 12, NULL, 'Soft Skills', 1, GETUTCDATE()),
    (13, 'Effective Communication Skills', 'COM-SKL-001', 'Professional communication in the workplace', 8, NULL, 'Soft Skills', 1, GETUTCDATE()),
    (14, 'Project Management Professional', 'PM-PRO-001', 'Comprehensive project management methodology', 40, 36, 'Management', 1, GETUTCDATE()),
    (15, 'Lean Six Sigma Yellow Belt', 'LSS-YB-001', 'Introduction to Lean and Six Sigma principles', 16, 24, 'Operational', 1, GETUTCDATE());

SET IDENTITY_INSERT [dbo].[TrainingCourses] OFF;

-- Seed Employees
SET IDENTITY_INSERT [dbo].[Employees] ON;

INSERT INTO [dbo].[Employees] (Id, FirstName, LastName, Email, EmployeeNumber, DepartmentId, HireDate, IsActive, CreatedDate)
VALUES 
    (1, 'John', 'Smith', 'john.smith@company.com', 'EMP001', 5, '2020-01-15', 1, GETUTCDATE()),
    (2, 'Sarah', 'Johnson', 'sarah.johnson@company.com', 'EMP002', 5, '2019-06-01', 1, GETUTCDATE()),
    (3, 'Michael', 'Williams', 'michael.williams@company.com', 'EMP003', 6, '2021-03-10', 1, GETUTCDATE()),
    (4, 'Emily', 'Brown', 'emily.brown@company.com', 'EMP004', 7, '2018-09-20', 1, GETUTCDATE()),
    (5, 'David', 'Jones', 'david.jones@company.com', 'EMP005', 7, '2020-11-05', 1, GETUTCDATE()),
    (6, 'Jessica', 'Garcia', 'jessica.garcia@company.com', 'EMP006', 8, '2019-02-14', 1, GETUTCDATE()),
    (7, 'Robert', 'Martinez', 'robert.martinez@company.com', 'EMP007', 4, '2017-07-22', 1, GETUTCDATE()),
    (8, 'Lisa', 'Anderson', 'lisa.anderson@company.com', 'EMP008', 9, '2021-05-18', 1, GETUTCDATE()),
    (9, 'James', 'Taylor', 'james.taylor@company.com', 'EMP009', 10, '2018-12-01', 1, GETUTCDATE()),
    (10, 'Jennifer', 'Thomas', 'jennifer.thomas@company.com', 'EMP010', 5, '2022-01-10', 1, GETUTCDATE()),
    (11, 'William', 'Moore', 'william.moore@company.com', 'EMP011', 7, '2019-08-15', 1, GETUTCDATE()),
    (12, 'Amanda', 'Jackson', 'amanda.jackson@company.com', 'EMP012', 4, '2020-04-22', 1, GETUTCDATE());

SET IDENTITY_INSERT [dbo].[Employees] OFF;

-- Seed Employee Skills
-- Note: ProficiencyLevel is INT (1-5) in your schema, not VARCHAR
INSERT INTO [dbo].[EmployeeSkills] (EmployeeId, SkillId, ProficiencyLevel, AcquiredDate, LastAssessedDate, Notes, CreatedDate)
VALUES 
    (1, 1, 5, '2015-01-01', '2024-12-01', 'Senior developer with 10+ years experience', GETUTCDATE()),
    (1, 2, 4, '2016-01-01', '2024-12-01', 'Database architecture specialist', GETUTCDATE()),
    (1, 3, 5, '2020-01-01', '2024-11-15', 'ASP.NET Core certified', GETUTCDATE()),
    (1, 5, 4, '2018-01-01', '2024-10-20', NULL, GETUTCDATE()),
    (2, 1, 4, '2019-06-01', '2024-11-20', 'Mid-level developer', GETUTCDATE()),
    (2, 3, 3, '2020-01-01', '2024-11-20', 'Currently improving', GETUTCDATE()),
    (2, 5, 4, '2019-06-01', '2024-11-01', NULL, GETUTCDATE()),
    (4, 6, 5, '2019-01-01', '2024-10-15', 'Certified machine operator', GETUTCDATE()),
    (4, 9, 4, '2018-09-20', '2024-09-01', 'Safety officer', GETUTCDATE()),
    (4, 15, 3, '2023-01-01', '2024-08-10', 'Lean champion', GETUTCDATE()),
    (5, 6, 4, '2020-11-05', '2024-10-10', 'Three years experience', GETUTCDATE()),
    (5, 9, 3, '2020-11-05', '2024-09-01', NULL, GETUTCDATE()),
    (6, 8, 5, '2019-02-14', '2024-11-05', 'Head of logistics', GETUTCDATE()),
    (6, 9, 4, '2019-02-14', '2024-09-01', NULL, GETUTCDATE()),
    (7, 7, 5, '2018-01-01', '2024-12-15', 'Lead QA inspector', GETUTCDATE()),
    (7, 14, 4, '2019-01-01', '2024-11-30', 'Risk assessment certified', GETUTCDATE());

-- Seed Employee Trainings
-- Note: Status is required, no TrainerName column in your schema
INSERT INTO [dbo].[EmployeeTrainings] (EmployeeId, TrainingCourseId, CompletionDate, ExpiryDate, Status, Score, CertificateNumber, Notes, CreatedDate)
VALUES 
    (1, 1, '2023-06-15', '2025-06-15', 'Completed', 95.5, 'CERT-CS-2023-001', 'Excellent performance - Trainer: Dr. Alan Cooper', GETUTCDATE()),
    (1, 3, '2023-09-20', '2025-03-20', 'Completed', 92.0, 'CERT-ASP-2023-045', 'Trainer: Jane Developer', GETUTCDATE()),
    (1, 4, '2024-01-10', '2026-01-10', 'Completed', 88.5, 'CERT-AZ-2024-012', 'Passed with distinction - Trainer: Mike Azure', GETUTCDATE()),
    (2, 1, '2024-03-15', '2026-03-15', 'Completed', 87.0, 'CERT-CS-2024-023', 'Trainer: Dr. Alan Cooper', GETUTCDATE()),
    (2, 2, '2024-06-01', '2025-06-01', 'Completed', 91.5, 'CERT-SQL-2024-056', 'Strong SQL skills - Trainer: Sarah Database', GETUTCDATE()),
    (4, 6, '2022-05-10', '2025-05-10', 'Completed', 94.0, 'CERT-CNC-2022-008', 'Certified operator - Trainer: Tom Machinist', GETUTCDATE()),
    (4, 10, '2024-08-15', '2025-08-15', 'Completed', 98.0, 'CERT-WHS-2024-134', 'Trainer: Safety Training Inc.', GETUTCDATE()),
    (4, 15, '2024-09-01', '2026-09-01', 'Completed', 85.0, 'CERT-LSS-2024-089', 'Yellow Belt achieved - Trainer: Lean Institute', GETUTCDATE()),
    (5, 6, '2023-11-20', '2026-11-20', 'Completed', 82.0, 'CERT-CNC-2023-167', 'Trainer: Tom Machinist', GETUTCDATE()),
    (6, 9, '2023-03-15', '2026-03-15', 'Completed', 96.0, 'CERT-FKL-2023-045', 'Licensed operator - Trainer: Forklift Academy', GETUTCDATE()),
    (6, 10, '2024-08-15', '2025-08-15', 'Completed', 94.5, 'CERT-WHS-2024-135', 'Trainer: Safety Training Inc.', GETUTCDATE()),
    (7, 8, '2022-07-10', '2023-07-10', 'Expired', 97.5, 'CERT-QC-2022-034', 'Lead inspector certified - Trainer: Quality Standards Ltd.', GETUTCDATE()),
    (7, 10, '2024-08-15', '2025-08-15', 'Completed', 99.0, 'CERT-WHS-2024-136', 'Perfect score - Trainer: Safety Training Inc.', GETUTCDATE()),
    (7, 11, '2023-10-05', '2025-10-05', 'Completed', 93.0, 'CERT-FA-2023-112', 'First aid certified - Trainer: Red Cross', GETUTCDATE());

PRINT 'Seed data inserted successfully!';
PRINT 'Departments: 10';
PRINT 'Skills: 15';
PRINT 'Training Courses: 15';
PRINT 'Employees: 12';
PRINT 'Employee Skills: 16';
PRINT 'Employee Trainings: 14';

-- Seed Department Skill Requirements for compliance dashboard
INSERT INTO [dbo].[DepartmentSkillRequirements] (DepartmentId, SkillId, RequiredCount, MinimumProficiencyLevel, Priority, IsActive, CreatedDate)
VALUES
    (5, 1, 2, 3, 'High', 1, GETUTCDATE()),
    (5, 3, 2, 3, 'High', 1, GETUTCDATE()),
    (5, 5, 1, 2, 'Medium', 1, GETUTCDATE()),
    (7, 6, 3, 3, 'Critical', 1, GETUTCDATE()),
    (7, 9, 2, 3, 'High', 1, GETUTCDATE()),
    (7, 15, 1, 2, 'Medium', 1, GETUTCDATE()),
    (8, 8, 2, 4, 'Critical', 1, GETUTCDATE()),
    (8, 9, 2, 3, 'High', 1, GETUTCDATE()),
    (4, 7, 2, 4, 'Critical', 1, GETUTCDATE()),
    (4, 14, 1, 3, 'High', 1, GETUTCDATE());

PRINT 'Department Skill Requirements: 10';