using System.Reflection;
using Dima.core.Models;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // aplica uma única configuração
        // modelBuilder.ApplyConfiguration()
        
        // aplica todas as configurações que implementa IEntityTypeConfiguration
        // varre todos os itens de um determinado assembly que implementa IEntityTypeConfiguration 
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}