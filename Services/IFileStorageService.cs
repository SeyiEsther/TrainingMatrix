namespace TrainingMatrixApp.Services;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string subfolder);
    Task DeleteFileAsync(string storagePath);
    bool FileExists(string storagePath);
}
