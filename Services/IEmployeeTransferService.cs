using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Services;

public interface IEmployeeTransferService
{
    Task TransferEmployeeAsync(int employeeId, int toDepartmentId, string reason, string transferredBy);
    Task<List<TransferHistory>> GetTransferHistoryAsync(int employeeId);
}
