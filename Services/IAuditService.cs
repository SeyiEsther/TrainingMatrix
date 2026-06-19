namespace TrainingMatrixApp.Services;

public interface IAuditService
{
    Task LogAsync(string actionType, string entityType, string entityId, string details);
}
