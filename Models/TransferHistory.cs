namespace TrainingMatrixApp.Models;

public class TransferHistory
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int FromDepartmentId { get; set; }
    public Department FromDepartment { get; set; } = null!;
    public int ToDepartmentId { get; set; }
    public Department ToDepartment { get; set; } = null!;
    public DateTime TransferDate { get; set; }
    public string? Reason { get; set; }
    public string TransferredBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}