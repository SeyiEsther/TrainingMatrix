using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=App_Data/TrainingMatrix.db";

var dbDirectory = Path.GetDirectoryName(connectionString.Replace("Data Source=", "", StringComparison.OrdinalIgnoreCase).Trim());
if (!string.IsNullOrEmpty(dbDirectory))
{
    Directory.CreateDirectory(dbDirectory);
}

builder.Services.AddDbContext<TrainingMatrixDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<ITrainingMatrixService, TrainingMatrixService>();
builder.Services.AddScoped<IEmployeeTransferService, EmployeeTransferService>();
builder.Services.AddScoped<DepartmentSkillComplianceService>();
builder.Services.AddScoped<IAuditService, AuditService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(7);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

if (OperatingSystem.IsWindows())
{
    builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
        .AddNegotiate();

    builder.Services.AddAuthorization(options =>
    {
        options.FallbackPolicy = options.DefaultPolicy;
    });
}
else
{
    builder.Services.AddAuthorization();
}

builder.Services.Configure<FileStorageOptions>(
    builder.Configuration.GetSection("FileStorage"));

builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();

var app = builder.Build();

var recreateDatabase = app.Configuration.GetValue<bool>("Database:RecreateOnStartup");
await DbInitializer.InitializeAsync(app.Services, recreateDatabase);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

if (OperatingSystem.IsWindows())
{
    app.UseAuthentication();
    app.UseAuthorization();
}

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
