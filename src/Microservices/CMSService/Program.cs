using Application.Implement;
using CMSService.Services;
using EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
string? connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContextPool<QueryDbContext>(option =>
{
    _ = option.UseNpgsql(connectionString, sql =>
    {
        _ = sql.MigrationsAssembly("Http.API");
        _ = sql.CommandTimeout(10);
    });
});
builder.Services.AddDbContextPool<CommandDbContext>(option =>
{
    _ = option.UseNpgsql(connectionString, sql =>
    {
        _ = sql.MigrationsAssembly("Http.API");
        _ = sql.CommandTimeout(10);
    });
});

builder.Services.AddDataStore();
builder.Services.AddManager();

var app = builder.Build();

app.Run();
