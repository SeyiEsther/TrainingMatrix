using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingMatrixApp.Models
{
    public class EmployeeTrainingTask
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        [ValidateNever]
        public Employee Employee { get; set; }

        [Required]
        [Display(Name = "Training Task")]
        public int TrainingTaskId { get; set; }

        [ValidateNever]
        public TrainingTask TrainingTask { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [ValidateNever]
        public Department Department { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; } = false;
        
        [Display(Name = "Completed Date")]
        public DateTime? CompletedDate { get; set; }

        public ICollection<EmployeeTrainingTaskSkill> EmployeeTrainingTaskSkills { get; set; } = new List<EmployeeTrainingTaskSkill>();

        // Non-Advanced Completion Signatures
        public string? NonAdvancedEmployeeSignature { get; set; }
        public DateTime? NonAdvancedEmployeeSignatureDate { get; set; }
        public string? NonAdvancedManagerSignature { get; set; }
        public DateTime? NonAdvancedManagerSignatureDate { get; set; }

        // Advanced Completion Signatures
        public string? AdvancedEmployeeSignature { get; set; }
        public DateTime? AdvancedEmployeeSignatureDate { get; set; }
        public string? AdvancedManagerSignature { get; set; }
        public DateTime? AdvancedManagerSignatureDate { get; set; }
    }
}