using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Avans.Identity.Dapper;
using Dapper;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Repository;
using ZorgWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICharacterRepository, CharacterRepository>();
builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddAuthorization();
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
        .GetConnectionString("ConnectionString"); // voor een localhost voeg in usersecret de connectionstring toe zie teams voor connectionstring
    });
var sqlConnectionString = builder.Configuration.GetValue<string>("ConnectionString");
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);

builder.Services.AddHttpContextAccessor(); 
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();

var app = builder.Build();
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


app.MapGroup("/account").MapIdentityApi<IdentityUser>();
app.MapControllers().RequireAuthorization();
app.UseSwagger();
app.UseSwaggerUI(); 
app.UseHttpsRedirection();
app.MapGet("/", () => $"The ZorgAppWebAPI is up. Connection string found: {(sqlConnectionStringFound ? "Yes" : "No")}");
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
