using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Avans.Identity.Dapper;
using Dapper;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Repository;
using ZorgWebApi.Services;
using System.Data;
using Microsoft.AspNetCore.DataProtection;
using System.IO;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container. //
var sqlConnectionString = builder.Configuration.GetValue<string>("connectionstring");
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);

builder.Services.AddControllers();

// Configure OpenAPI/Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Register application services
builder.Services.AddTransient<ICharacterRepository, CharacterRepository>();
builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ITipRepository, TipRepository>();
builder.Services.AddScoped<IDagboekRepository, DagboekRepository>();

// Configure authorization
builder.Services.AddAuthorization();

// Configure Identity with Dapper stores
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 10;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
    })
    .AddRoles<IdentityRole>()
    .AddDapperStores(options =>
    {
        options.ConnectionString = builder.Configuration
        .GetValue<string>("connectionstring"); // Add connection string in user secrets for localhost
    });

// Get the SQL connection string from configuration

// Register IDbConnection
builder.Services.AddTransient<IDbConnection>(sp => new SqlConnection(sqlConnectionString));

// Register other services
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();

var app = builder.Build();

// Endpoint for logging out
app.MapPost("/account/logout",
    async (SignInManager<IdentityUser> SignInManager,
    [FromBody] object empty) =>
    {
        if (empty != null)
        {
            await SignInManager.SignOutAsync();
            return Results.Ok();
        }
        return Results.Unauthorized();
    })
    .RequireAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Map identity and controller endpoints
app.MapGroup("/account").MapIdentityApi<IdentityUser>();
app.MapControllers().RequireAuthorization();

// Configure Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Configure HTTPS redirection
app.UseHttpsRedirection();

// Health check endpoint
app.MapGet("/", () => $"The ZorgAppWebAPI is up. Connection string found: {(sqlConnectionStringFound ? "Yes" : "No")}");

// Configure authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the application
app.Run();

