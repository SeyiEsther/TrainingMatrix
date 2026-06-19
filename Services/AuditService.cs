using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Services;

public class AuditService : IAuditService
{
    private readonly TrainingMatrixDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditService(TrainingMatrixDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LogAsync(string actionType, string entityType, string entityId, string details)
    {
        _context.AuditLogs.Add(new AuditLog
        {
            Timestamp = DateTime.UtcNow,
            ActionType = actionType,
            EntityType = entityType,
            EntityId = entityId,
            Details = details,
            PerformedBy = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System"
        });

        await _context.SaveChangesAsync();
    }
}
