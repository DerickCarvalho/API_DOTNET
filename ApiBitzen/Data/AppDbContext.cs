using ApiBitzen.Usuarios;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace ApiBitzen.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Env.Load();

            string? connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

            if (!string.IsNullOrEmpty(connectionString)) {
                optionsBuilder.UseNpgsql(connectionString);
            }
            else {
                throw new Exception("Connection string não encontrada!");
            }
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
    }
}