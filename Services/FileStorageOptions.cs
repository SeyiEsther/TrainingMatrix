namespace TrainingMatrixApp.Services;

public class FileStorageOptions
{
    public string BasePath { get; set; } = "App_Data/uploads/training";
    public int MaxFileSizeMB { get; set; } = 50;
    public string[] AllowedExtensions { get; set; } = [".pdf", ".doc", ".docx"];
}
