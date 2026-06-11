using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Services;

public class EmployeeTransferService : IEmployeeTransferService
{
    private readonly TrainingMatrixDbContext _context;

    public EmployeeTransferService(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public async Task TransferEmployeeAsync(int employeeId, int toDepartmentId, string reason, string transferredBy)
    {
        var employee = await _context.Employees.FindAsync(employeeId)
            ?? throw new InvalidOperationException($"Employee with ID {employeeId} not found.");

        var toDepartment = await _context.Departments.FindAsync(toDepartmentId)
            ?? throw new InvalidOperationException($"Department with ID {toDepartmentId} not found.");

        var fromDepartmentId = employee.DepartmentId;

        var transfer = new TransferHistory
        {
            EmployeeId = employeeId,
            FromDepartmentId = fromDepartmentId,
            ToDepartmentId = toDepartmentId,
            TransferDate = DateTime.UtcNow,
            Reason = reason,
            TransferredBy = transferredBy,
            CreatedDate = DateTime.UtcNow
        };

        employee.DepartmentId = toDepartmentId;

        _context.TransferHistory.Add(transfer);

        var auditLog = new AuditLog
        {
            Timestamp = DateTime.UtcNow,
            ActionType = "Transfer",
            EntityType = "Employee",
            EntityId = employee.EmployeeNumber,
            Details = $"Employee {employee.FullName} transferred from department {fromDepartmentId} to {toDepartment.Name}. Reason: {reason}",
            PerformedBy = transferredBy
        };
        _context.AuditLogs.Add(auditLog);

        await _context.SaveChangesAsync();
    }

    public async Task<List<TransferHistory>> GetTransferHistoryAsync(int employeeId)
    {
        return await _context.TransferHistory
            .Where(th => th.EmployeeId == employeeId)
            .Include(th => th.FromDepartment)
            .Include(th => th.ToDepartment)
            .OrderByDescending(th => th.TransferDate)
            .ToListAsync();
    }
}
