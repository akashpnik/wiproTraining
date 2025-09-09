using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using System;
using SecureWebApp.Controllers; // Add this using directive for DTOs and ViewModels
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// --- Section 1: Azure Key Vault for Production ---
// This block configures the application to pull secrets from Azure Key Vault
// when running in the "Production" environment.
if (builder.Environment.IsProduction())
{
    var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("KEY_VAULT_ENDPOINT"));
    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
}

// --- Section 2: Register Services for Dependency Injection ---

// Register the DataEncryptionService as a singleton so the key/IV are loaded once.
builder.Services.AddSingleton<DataEncryptionService>();

// Register the AuthService as a scoped service.
builder.Services.AddScoped<AuthService>();

// Configure and register the ApplicationDbContext for Entity Framework Core.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add standard API services.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5290; // pick your HTTPS port
});

// --- Section 3: Build and Configure the Application ---

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Enable authentication and authorization middleware.
// (Further configuration is needed to set up JWT or Cookie authentication)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
