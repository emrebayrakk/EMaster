using EMaster.API.OptionsSetup;
using EMaster.Application.Authentication;
using EMaster.Application.Category;
using EMaster.Application.Expense;
using EMaster.Application.Income;
using EMaster.Application.User;
using EMaster.Data.Context;
using EMaster.Data.Mappings;
using EMaster.Data.Repositories;
using EMaster.Data.Repositories.EntityFramework;
using EMaster.Domain.Interfaces;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.AddDbContext<EMasterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

#region DI
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICategoryRepo,CategoryRepo>();
builder.Services.AddScoped<ICategoryService,CategoryService>();

builder.Services.AddScoped<IExpenseRepo, ExpenseRepo>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services.AddScoped<IIncomeRepo, IncomeRepo>();
builder.Services.AddScoped<IIncomeService, IncomeService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
#endregion




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpcontext.Request.Headers.Host.ToString(),
        factory: partition => new FixedWindowRateLimiterOptions
        {
            AutoReplenishment = true,
            PermitLimit = 3,
            Window = TimeSpan.FromMilliseconds(3000)
        }
    ));

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        ApiResponse<string> apiResponse = new ApiResponse<string>(
                false,
                429,
                "Error",
                ""
            );

        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            apiResponse.Data = $"Ýstek sýnýr sayýsýna ulaþtýnýz. {retryAfter.TotalMinutes} dakika sonra tekrar deneyiniz.";
            string jsonResponse = System.Text.Json.JsonSerializer.Serialize(apiResponse);
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(jsonResponse, cancellationToken: token);
        }
        else
        {
            apiResponse.Data = "Ýstek sýnýrýna ulaþtýnýz. Daha sonra tekrar deneyin.";
            string jsonResponse = System.Text.Json.JsonSerializer.Serialize(apiResponse);
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(jsonResponse, cancellationToken: token);
        }
    };
});

MapsterConfiguration.ConfigureMappings();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTheme(ScalarTheme.Moon)
            .WithDarkMode(true)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithDarkModeToggle(false);
    });
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();

app.Run();
