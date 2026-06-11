using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrainingMatrixApp.Models;

public class Employee
{
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "Employee Number")]
    [StringLength(20)]
    public string EmployeeNumber { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "First Name")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Last Name")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
    
    [Phone]
    [Display(Name = "Phone Number")]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }
    
    [Required]
    [Display(Name = "Department")]
    public int DepartmentId { get; set; }
    
    [ValidateNever]
    public Department Department { get; set; } = null!;
    [Display(Name = "Shift")]
    public int? Shift { get; set; }
    
    [ValidateNever]
    public ICollection<EmployeeTraining> Trainings { get; set; } = new List<EmployeeTraining>();
    
    [ValidateNever]
    public ICollection<EmployeeSkill> Skills { get; set; } = new List<EmployeeSkill>();
    
    [ValidateNever]
    public ICollection<TransferHistory> TransferHistory { get; set; } = new List<TransferHistory>();
    
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
    
    [Required]
    [Display(Name = "Hire Date")]
    [DataType(DataType.Date)]
    public DateTime HireDate { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    [Display(Name = "Full Name")]
    public string FullName => $"{FirstName} {LastName}";
}