using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrainingMatrixApp.Models;

public class TrainingCourse
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    [Display(Name = "Course Name")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [StringLength(100)]
    [Display(Name = "Course Code")]
    public string? CourseCode { get; set; }
    
    [StringLength(100)]
    public string? Provider { get; set; }
    
    [Display(Name = "Duration (Hours)")]
    [Range(0, 1000)]
    public int? DurationHours { get; set; }
    
    [Display(Name = "Validity Period (Months)")]
    [Range(0, 120)]
    public int? ValidityMonths { get; set; }
    
    [Display(Name = "Passing Score")]
    [Range(0, 100)]
    public decimal? PassingScore { get; set; }
    
    [StringLength(50)]
    public string? Category { get; set; }
    
    [Display(Name = "Cost")]
    [DataType(DataType.Currency)]
    public decimal? Cost { get; set; }
    
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
    
    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    [ValidateNever]
    public ICollection<EmployeeTraining> EmployeeTrainings { get; set; } = new List<EmployeeTraining>();
}