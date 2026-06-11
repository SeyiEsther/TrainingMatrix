namespace TrainingMatrixApp.ViewModels;

public class DepartmentSkillComplianceViewModel
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int SkillId { get; set; }
    public string SkillName { get; set; } = string.Empty;
    public int RequiredCount { get; set; }
    public int MinimumProficiencyLevel { get; set; }
    public string Priority { get; set; } = string.Empty;
    public int CurrentQualifiedCount { get; set; }
    public int ShortfallCount { get; set; }
    public string ComplianceStatus { get; set; } = string.Empty;
    public decimal CompliancePercentage { get; set; }
    
    public string GetStatusBadgeClass()
    {
        return ComplianceStatus switch
        {
            "Met" => "bg-success",
            "Nearly Met" => "bg-warning",
            _ => "bg-danger"
        };
    }
    
    public string GetPriorityBadgeClass()
    {
        return Priority switch
        {
            "Critical" => "bg-danger",
            "High" => "bg-warning",
            "Medium" => "bg-info",
            _ => "bg-secondary"
        };
    }
}