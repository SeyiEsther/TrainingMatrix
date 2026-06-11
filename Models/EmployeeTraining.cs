using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrainingMatrixApp.Models;

public class EmployeeTraining
{
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "Employee")]
    public int EmployeeId { get; set; }
    
    [ValidateNever]
    public Employee Employee { get; set; } = null!;
    
    [Required]
    [Display(Name = "Training Course")]
    public int TrainingCourseId { get; set; }
    
    [ValidateNever]
    public TrainingCourse TrainingCourse { get; set; } = null!;
    
    [Required]
    [Display(Name = "Completion Date")]
    [DataType(DataType.Date)]
    public DateTime CompletionDate { get; set; }
    
    [Display(Name = "Expiry Date")]
    [DataType(DataType.Date)]
    public DateTime? ExpiryDate { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "Completed";
    
    [Range(0, 100)]
    public decimal? Score { get; set; }
    
    [StringLength(1000)]
    public string? Notes { get; set; }
    
    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    [ValidateNever]
    public ICollection<TrainingAttachment> Attachments { get; set; } = new List<TrainingAttachment>();
}