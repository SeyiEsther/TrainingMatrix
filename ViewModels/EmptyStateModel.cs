namespace TrainingMatrixApp.ViewModels;

public class EmptyStateModel
{
    public string Icon { get; set; } = "bi-inbox";
    public string Message { get; set; } = string.Empty;
    public string? ActionText { get; set; }
    public string? ActionUrl { get; set; }
}
