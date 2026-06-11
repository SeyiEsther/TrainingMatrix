using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingMatrixApp.Models
{
    public class EmployeeTrainingTaskSkill
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeTrainingTaskId { get; set; }
        public EmployeeTrainingTask EmployeeTrainingTask { get; set; }

        [Required]
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        [Display(Name = "Status")]
        [StringLength(50)]
        public string? Status { get; set; } = "Not Started"; // Not Started, In Progress, Passed, Failed

        [Display(Name = "Started Date")]
        public DateTime? StartedDate { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; } = false;
        
        [Display(Name = "Completed Date")]
        public DateTime? CompletedDate { get; set; }

        [Display(Name = "Notes")]
        [StringLength(500)]
        public string? Notes { get; set; }
    }
}