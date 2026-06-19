using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Models;
using TrainingTask = TrainingMatrixApp.Models.TrainingTask;
using TrainingTaskSkill = TrainingMatrixApp.Models.TrainingTaskSkill;

namespace TrainingMatrixApp.Data;

public class TrainingMatrixDbContext : DbContext
{
    public TrainingMatrixDbContext(DbContextOptions<TrainingMatrixDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<TrainingCourse> TrainingCourses { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<EmployeeTraining> EmployeeTrainings { get; set; }
    public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
    public DbSet<DepartmentSkillRequirement> DepartmentSkillRequirements { get; set; }
    public DbSet<TransferHistory> TransferHistory { get; set; }
    public DbSet<TrainingAttachment> TrainingAttachments { get; set; }

    public DbSet<TrainingTask> TrainingTasks { get; set; }
    public DbSet<TrainingTaskSkill> TrainingTaskSkills { get; set; }
    public DbSet<EmployeeTrainingTask> EmployeeTrainingTasks { get; set; }
    public DbSet<EmployeeTrainingTaskSkill> EmployeeTrainingTaskSkills { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Department configuration
        modelBuilder.Entity<Department>()
            .HasOne(d => d.ParentDepartment)
            .WithMany(d => d.SubDepartments)
            .HasForeignKey(d => d.ParentDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Department>()
            .HasOne(d => d.HeadOfDepartment)
            .WithMany()
            .HasForeignKey(d => d.HeadOfDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Employee configuration
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.EmployeeNumber)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();

        // Skill configuration
        modelBuilder.Entity<Skill>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<Skill>()
            .HasIndex(s => s.Category);

        modelBuilder.Entity<Skill>()
            .Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Skill>()
            .Property(s => s.Category)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Skill>()
            .Property(s => s.Description)
            .HasMaxLength(500);

        // DepartmentSkillRequirement configuration
        modelBuilder.Entity<DepartmentSkillRequirement>()
            .HasOne(dsr => dsr.Department)
            .WithMany(d => d.SkillRequirements)
            .HasForeignKey(dsr => dsr.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DepartmentSkillRequirement>()
            .HasOne(dsr => dsr.Skill)
            .WithMany(s => s.DepartmentRequirements)
            .HasForeignKey(dsr => dsr.SkillId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ensure unique department-skill requirement combination
        modelBuilder.Entity<DepartmentSkillRequirement>()
            .HasIndex(dsr => new { dsr.DepartmentId, dsr.SkillId })
            .IsUnique();

        // Add check constraints
        modelBuilder.Entity<DepartmentSkillRequirement>()
            .ToTable(t => t.HasCheckConstraint("CK_DepartmentSkillRequirement_RequiredCount", 
                "[RequiredCount] >= 1 AND [RequiredCount] <= 100"));

        modelBuilder.Entity<DepartmentSkillRequirement>()
            .ToTable(t => t.HasCheckConstraint("CK_DepartmentSkillRequirement_MinimumProficiencyLevel", 
                "[MinimumProficiencyLevel] >= 1 AND [MinimumProficiencyLevel] <= 5"));

        modelBuilder.Entity<DepartmentSkillRequirement>()
            .ToTable(t => t.HasCheckConstraint("CK_DepartmentSkillRequirement_Priority", 
                "[Priority] IN ('Low', 'Medium', 'High', 'Critical')"));

        modelBuilder.Entity<DepartmentSkillRequirement>()
            .Property(dsr => dsr.Priority)
            .HasMaxLength(20)
            .IsRequired()
            .HasDefaultValue("Medium");

        modelBuilder.Entity<DepartmentSkillRequirement>()
            .Property(dsr => dsr.Notes)
            .HasMaxLength(500);

        // EmployeeTraining configuration
        modelBuilder.Entity<EmployeeTraining>()
            .HasOne(et => et.Employee)
            .WithMany(e => e.Trainings)
            .HasForeignKey(et => et.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EmployeeTraining>()
            .HasOne(et => et.TrainingCourse)
            .WithMany(tc => tc.EmployeeTrainings)
            .HasForeignKey(et => et.TrainingCourseId)
            .OnDelete(DeleteBehavior.Restrict);

        // EmployeeSkill configuration
        modelBuilder.Entity<EmployeeSkill>()
            .HasOne(es => es.Employee)
            .WithMany(e => e.Skills)
            .HasForeignKey(es => es.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EmployeeSkill>()
            .HasOne(es => es.Skill)
            .WithMany(s => s.EmployeeSkills)
            .HasForeignKey(es => es.SkillId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ensure unique employee-skill combination
        modelBuilder.Entity<EmployeeSkill>()
            .HasIndex(es => new { es.EmployeeId, es.SkillId })
            .IsUnique();

        // Add check constraint for proficiency level (1-5)
        modelBuilder.Entity<EmployeeSkill>()
            .ToTable(t => t.HasCheckConstraint("CK_EmployeeSkill_ProficiencyLevel", 
                "[ProficiencyLevel] >= 1 AND [ProficiencyLevel] <= 5"));

        modelBuilder.Entity<EmployeeSkill>()
            .Property(es => es.Notes)
            .HasMaxLength(500);

        // TransferHistory configuration
        modelBuilder.Entity<TransferHistory>()
            .HasOne(th => th.Employee)
            .WithMany(e => e.TransferHistory)
            .HasForeignKey(th => th.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TransferHistory>()
            .HasOne(th => th.FromDepartment)
            .WithMany()
            .HasForeignKey(th => th.FromDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TransferHistory>()
            .HasOne(th => th.ToDepartment)
            .WithMany()
            .HasForeignKey(th => th.ToDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // TrainingTasks configuration
        modelBuilder.Entity<TrainingTask>()
            .ToTable("TrainingTasks");

        // TrainingTaskSkills configuration
        modelBuilder.Entity<TrainingTaskSkill>()
            .ToTable("TrainingTaskSkills")
            .HasKey(ts => new { ts.TrainingTaskId, ts.SkillId });

        modelBuilder.Entity<TrainingTaskSkill>()
            .HasOne(ts => ts.TrainingTask)
            .WithMany(t => t.TrainingTaskSkills)
            .HasForeignKey(ts => ts.TrainingTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TrainingTaskSkill>()
            .HasOne(ts => ts.Skill)
            .WithMany()
            .HasForeignKey(ts => ts.SkillId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EmployeeTrainingTask>()
            .HasOne(ett => ett.Department)
            .WithMany()
            .HasForeignKey(ett => ett.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AuditLog>()
            .Property(a => a.ActionType)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<AuditLog>()
            .Property(a => a.EntityType)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<AuditLog>()
            .Property(a => a.EntityId)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<AuditLog>()
            .Property(a => a.PerformedBy)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Entity<AuditLog>()
            .HasIndex(a => a.Timestamp);

        modelBuilder.Entity<AuditLog>()
            .HasIndex(a => new { a.EntityType, a.EntityId });
    }
}