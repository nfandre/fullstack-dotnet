using Dima.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");
        builder.HasKey(transaction => transaction.Id);
        
        builder.Property(transaction => transaction.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(transaction => transaction.Type)
            .IsRequired()
            .HasColumnType("SMALLINT");

        builder.Property(transaction => transaction.Amount)
            .IsRequired()
            .HasColumnType("MONEY");
        
        builder.Property(transaction => transaction.CreatedAt)
            .IsRequired();
        
        builder.Property(transaction => transaction.PaidOrReceivedAt)            
            .IsRequired(false);

        builder.Property(transaction => transaction.UserId)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}