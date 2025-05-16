using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace BookService.Models.Db;

public class DbOrder
{
    public const string TableName = "Orders";

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = "Pending"; // Храним как строку
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? OrderFulfillmentDate { get; set; }

    [IgnoreParse]
    public ICollection<DbOrderBook> OrderBooks { get; set; }
    public DateTime ModifiedAt { get; set; }

    public DbOrder()
    {
        OrderBooks = new HashSet<DbOrderBook>();
    }
}

public class DbOrderConfiguration : IEntityTypeConfiguration<DbOrder>
{
    public void Configure(EntityTypeBuilder<DbOrder> builder)
    {
        builder
            .ToTable(DbOrder.TableName);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.UserId)
            .IsRequired();

        builder
            .Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.IsCompleted)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .Property(x => x.OrderFulfillmentDate)
            .IsRequired(false);

        builder
            .HasMany(x => x.OrderBooks)
            .WithOne(ob => ob.Order)
            .HasForeignKey(ob => ob.OrderId);
    }
}