using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace BookService.Models.Db;

public class DbBookCart
{
    public const string TableName = "BookCarts";

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public int Quantity { get; set; }

    [IgnoreParse]
    public DbBook Book { get; set; } = null!;

    public DbBookCart()
    {
    }
}

public class DbBookCartConfiguration : IEntityTypeConfiguration<DbBookCart>
{
    public void Configure(EntityTypeBuilder<DbBookCart> builder)
    {
        builder
            .ToTable(DbBookCart.TableName);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.UserId)
            .IsRequired();

        builder
            .Property(x => x.BookId)
            .IsRequired();

        builder
            .Property(x => x.Quantity)
            .IsRequired();

        builder
            .HasOne(x => x.Book)
            .WithMany(b => b.BookCarts)
            .HasForeignKey(x => x.BookId);
    }
}