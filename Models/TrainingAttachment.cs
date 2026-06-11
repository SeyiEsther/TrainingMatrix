using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrainingMatrixApp.Models;

public class TrainingAttachment
{
    public int Id { get; set; }
    
    [Required]
    public int EmployeeTrainingId { get; set; }
    
    [ValidateNever]
    public EmployeeTraining EmployeeTraining { get; set; } = null!;
    
    [Required]
    [StringLength(255)]
    [Display(Name = "File Name")]
    public string FileName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    [Display(Name = "Storage Path")]
    public string StoragePath { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    [Display(Name = "Content Type")]
    public string ContentType { get; set; } = string.Empty;
    
    [Display(Name = "File Size")]
    public long FileSizeBytes { get; set; }
    
    [Required]
    [StringLength(50)]
    [Display(Name = "Attachment Type")]
    public string AttachmentType { get; set; } = "Certificate";
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [StringLength(100)]
    [Display(Name = "Uploaded By")]
    public string UploadedBy { get; set; } = string.Empty;
    
    [Display(Name = "Upload Date")]
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
    
    public string FileSizeFormatted
    {
        get
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = FileSizeBytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}