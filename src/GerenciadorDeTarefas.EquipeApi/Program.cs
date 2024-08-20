using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using TaskManager.TeamApi.Domain.Migrations;
using TaskManager.TeamApi.Infra.DB;
using TaskManager.TeamApi.Infra.Repository;
using ConfigurationManager = System.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);

string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TeamRepository>();

builder.Services
.AddFluentMigratorCore()
.ConfigureRunner(rb => rb
                 .AddSqlServer()
                 .WithGlobalConnectionString(connectionString)
                 .ScanIn(typeof(_2024081501_create_team_table).Assembly).For.Migrations()
                )
.AddLogging(lb => lb.AddFluentMigratorConsole());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    migrationRunner.MigrateUp();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
