using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.data
{
  public class AppDbContext : DbContext
  {
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
  }
}