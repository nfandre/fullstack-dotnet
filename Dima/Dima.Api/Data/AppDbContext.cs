using System.Reflection;
using Dima.Api.Models;
using Dima.core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;
// public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<
        User, // Tabela AspNetUsers
        IdentityRole<long>, // Tabela AspNetRoles ( junção AspNetUserRoles)
        long, // Tabelas, Claim, login, RoleClaim, Tokens de sessões (Modo automático)
        IdentityUserClaim<long>,
        IdentityUserRole<long>,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>
    >(options)
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