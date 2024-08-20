using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.TaskApi.Domain.Migrations;
using TaskManager.TaskApi.Infra.DB;
using TaskManager.TaskApi.Infra.Repository;

namespace TaskManager.TeamApi.Test
{
    public class BaseTest
    {
        public IServiceProvider CreateServices(string dbName)
        {
            DeleteDatabase(dbName);
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbName}"));

            services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString($"Data Source={dbName}")
                    .ScanIn(typeof(_2024081502_create_task_table).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            services.AddScoped<TaskRepository>();

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
                try
                {
                    var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                    migrationRunner.MigrateUp();
                }
                catch (Exception)
                {
                }
            }

            return serviceProvider;
        }

        public void DeleteDatabase(string dbName)
        {
            try
            {
                if (File.Exists(dbName))
                {
                    File.Delete(dbName);
                    Console.WriteLine($"Banco de dados '{dbName}' foi deletado com sucesso.");
                }
                else
                {
                    Console.WriteLine($"O arquivo de banco de dados '{dbName}' não existe.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro ao tentar deletar o banco de dados: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Acesso negado ao tentar deletar o banco de dados: {ex.Message}");
            }
        }
    }
}
