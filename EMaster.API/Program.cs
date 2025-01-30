using EMaster.API.Middlewares;
using EMaster.API.OptionsSetup;
using EMaster.Application.Authentication;
using EMaster.Application.Category;
using EMaster.Application.Company;
using EMaster.Application.Expense;
using EMaster.Application.Income;
using EMaster.Application.User;
using EMaster.Data.Context;
using EMaster.Data.Mappings;
using EMaster.Data.Repositories;
using EMaster.Data.Repositories.EntityFramework;
using EMaster.Domain.Interfaces;
using EMaster.Domain.Interfaces.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

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

builder.Services.AddScoped<ICompanyRepo, CompanyRepo>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
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

var rateLimiterMiddleware = new RateLimiterMiddleware(null);
rateLimiterMiddleware.ConfigureRateLimiter(builder.Services);

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
