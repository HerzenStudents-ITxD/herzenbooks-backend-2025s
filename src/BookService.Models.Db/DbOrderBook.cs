using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace BookService.Models.Db;

public class DbOrderBook
{
    public const string TableName = "OrderBooks";

    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid BookId { get; set; }
    public bool IsCompleted { get; set; }
    public int Quantity { get; set; }
    public DateTime? OrderFulfillmentDate { get; set; }

    [IgnoreParse]
    public DbOrder Order { get; set; } = null!;
    [IgnoreParse]
    public DbBook Book { get; set; } = null!;

    public DbOrderBook()
    {
    }
}

public class DbOrderBookConfiguration : IEntityTypeConfiguration<DbOrderBook>
{
    public void Configure(EntityTypeBuilder<DbOrderBook> builder)
    {
        builder
            .ToTable(DbOrderBook.TableName);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.OrderId)
            .IsRequired();

        builder
            .Property(x => x.BookId)
            .IsRequired();

        builder
            .Property(x => x.IsCompleted)
            .IsRequired();

        builder
            .Property(x => x.Quantity)
            .IsRequired();

        builder
            .Property(x => x.OrderFulfillmentDate)
            .IsRequired(false);

        builder
            .HasOne(x => x.Order)
            .WithMany(o => o.OrderBooks)
            .HasForeignKey(x => x.OrderId);

        builder
            .HasOne(x => x.Book)
            .WithMany(b => b.OrderBooks)
            .HasForeignKey(x => x.BookId);
    }
}