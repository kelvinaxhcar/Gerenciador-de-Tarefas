using Microsoft.EntityFrameworkCore;
using TaskManager.TeamApi.Domain.Models;

namespace TaskManager.TeamApi.Infra.DB;

public class AppDbContext : DbContext {
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

  public DbSet<Team> Team { get; set; } = null!;
}
