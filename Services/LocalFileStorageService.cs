using Microsoft.Extensions.Options;

namespace TrainingMatrixApp.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _baseUploadPath;
    private readonly IWebHostEnvironment _env;

    public LocalFileStorageService(IWebHostEnvironment env, IOptions<FileStorageOptions> options)
    {
        _env = env;
        _baseUploadPath = Path.Combine(_env.WebRootPath, "uploads", "training");
    }

    public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
    {
        var folderPath = Path.Combine(_baseUploadPath, subfolder);
        Directory.CreateDirectory(folderPath);

        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(folderPath, uniqueFileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        // Return relative path for storage in DB
        return Path.Combine("uploads", "training", subfolder, uniqueFileName).Replace('\\', '/');
    }

    public Task DeleteFileAsync(string storagePath)
    {
        var fullPath = Path.Combine(_env.WebRootPath, storagePath.TrimStart('/'));
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        return Task.CompletedTask;
    }

    public bool FileExists(string storagePath)
    {
        var fullPath = Path.Combine(_env.WebRootPath, storagePath.TrimStart('/'));
        return File.Exists(fullPath);
    }
}
