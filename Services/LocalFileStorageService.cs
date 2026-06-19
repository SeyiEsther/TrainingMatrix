using Microsoft.Extensions.Options;

namespace TrainingMatrixApp.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _baseUploadPath;
    private readonly FileStorageOptions _options;

    public LocalFileStorageService(IWebHostEnvironment env, IOptions<FileStorageOptions> options)
    {
        _options = options.Value;
        _baseUploadPath = Path.IsPathRooted(_options.BasePath)
            ? _options.BasePath
            : Path.Combine(env.ContentRootPath, _options.BasePath);
    }

    public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
    {
        ValidateFile(file);
        var safeSubfolder = SanitizeSubfolder(subfolder);

        var folderPath = Path.Combine(_baseUploadPath, safeSubfolder);
        Directory.CreateDirectory(folderPath);

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(folderPath, uniqueFileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return Path.Combine(_options.BasePath, safeSubfolder, uniqueFileName).Replace('\\', '/');
    }

    public Task DeleteFileAsync(string storagePath)
    {
        var fullPath = ResolveStoragePath(storagePath);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }

    public bool FileExists(string storagePath)
    {
        return File.Exists(ResolveStoragePath(storagePath));
    }

    private void ValidateFile(IFormFile file)
    {
        if (file.Length == 0)
        {
            throw new InvalidOperationException("The uploaded file is empty.");
        }

        var maxBytes = _options.MaxFileSizeMB * 1024L * 1024L;
        if (file.Length > maxBytes)
        {
            throw new InvalidOperationException($"File exceeds the maximum size of {_options.MaxFileSizeMB} MB.");
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_options.AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"File type '{extension}' is not allowed.");
        }
    }

    private static string SanitizeSubfolder(string subfolder)
    {
        var segments = subfolder
            .Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(segment => Path.GetFileName(segment))
            .Where(segment => !string.IsNullOrWhiteSpace(segment))
            .ToArray();

        if (segments.Length == 0)
        {
            throw new InvalidOperationException("A valid upload subfolder is required.");
        }

        return Path.Combine(segments);
    }

    private string ResolveStoragePath(string storagePath)
    {
        var relativePath = storagePath.TrimStart('/', '\\');
        if (Path.IsPathRooted(relativePath))
        {
            throw new InvalidOperationException("Storage path must be relative.");
        }

        var fullPath = Path.GetFullPath(Path.Combine(_baseUploadPath, relativePath));
        if (!fullPath.StartsWith(_baseUploadPath, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Invalid storage path.");
        }

        return fullPath;
    }
}
