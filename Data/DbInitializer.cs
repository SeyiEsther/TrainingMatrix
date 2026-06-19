using Microsoft.EntityFrameworkCore;

namespace TrainingMatrixApp.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services, bool recreate = false)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TrainingMatrixDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TrainingMatrixDbContext>>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Data Source=App_Data/TrainingMatrix.db";
        var dbPath = ExtractSqlitePath(connectionString);

        if (recreate && !string.IsNullOrEmpty(dbPath) && File.Exists(dbPath))
        {
            logger.LogWarning("Recreating local database at {DbPath}", dbPath);
            await context.Database.CloseConnectionAsync();
            File.Delete(dbPath);
        }

        await context.Database.MigrateAsync();

        if (!await context.Departments.AnyAsync())
        {
            logger.LogInformation("Seeding local database...");
            await DatabaseSeeder.SeedAsync(context);
            logger.LogInformation("Database seed completed.");
        }
    }

    private static string? ExtractSqlitePath(string connectionString)
    {
        var parts = connectionString.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var part in parts)
        {
            if (part.StartsWith("Data Source=", StringComparison.OrdinalIgnoreCase))
            {
                return part["Data Source=".Length..].Trim();
            }
        }

        return null;
    }
}
