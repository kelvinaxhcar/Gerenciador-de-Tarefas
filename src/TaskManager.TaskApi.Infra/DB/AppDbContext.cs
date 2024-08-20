using Microsoft.EntityFrameworkCore;

namespace TaskManager.TaskApi.Infra.DB;

public class AppDbContext : DbContext {
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

  public DbSet<Domain.Models.Task> Task { get; set; } = null!;
}
