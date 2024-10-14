using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.core;
using Dima.core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Common.Api;

// Wrapper
public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder
            .Configuration
            .GetConnectionString("DefaultConnection") ?? String.Empty;

        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName));
    }
    
    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        // Não é possível inverter essa ordem
        // Primeiro atenticação, dps autorização
        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies(); // Quem você é? Quem o usuário é? JWT
        builder.Services.AddAuthorization(); // O que você pode fazer? Perfis/ roles/ claims
    }
    
    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(Configuration.ConnectionString);
        });
        
        builder.Services
            .AddIdentityCore<User>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints(); // dotnet 8 api endpoints
    }
    
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }
    
    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => options.AddPolicy(
            ApiConfiguration.CorsPolicyName,
            policy => policy
                .WithOrigins([
                        Configuration.BackendUrl, 
                        Configuration.FrontendUrl
                    ])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            ));
    }
}