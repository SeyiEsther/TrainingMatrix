using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingMatrixApp.Models
{
    public class TrainingTask
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Task Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }


        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;


        [Display(Name = "Matrix Sort Order")]
        public int? SortOrder { get; set; }

        public ICollection<TrainingTaskSkill> TrainingTaskSkills { get; set; } = new List<TrainingTaskSkill>();

        [Display(Name = "Target Employee Count")]
        public int? TargetEmployeeCount { get; set; }
    }
}