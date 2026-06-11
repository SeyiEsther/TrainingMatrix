public class AuditLog
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string ActionType { get; set; } // e.g., "Add", "Update", "Move"
    public string EntityType { get; set; } // e.g., "Employee", "Department"
    public string EntityId { get; set; }   // EmployeeNumber or DepartmentId
    public string Details { get; set; }    // JSON or text description of the change
    public string PerformedBy { get; set; } // Optional: user who performed the action
}