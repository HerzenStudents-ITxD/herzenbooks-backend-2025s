using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace BookService.Models.Db;

public class DbBook
{
    public const string TableName = "Books";

    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ShortDescription { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? Photo { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime PublicationDate { get; set; }
    public int? Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Guid ModifiedBy { get; set; }

    [IgnoreParse]
    public DbDepartment? Department { get; set; }
    [IgnoreParse]
    public ICollection<DbBookAuthor> BookAuthors { get; set; }
    [IgnoreParse]
    public ICollection<DbBookCart> BookCarts { get; set; }
    [IgnoreParse]
    public ICollection<DbOrderBook> OrderBooks { get; set; }

    public DbBook()
    {
        BookAuthors = new HashSet<DbBookAuthor>();
        BookCarts = new HashSet<DbBookCart>();
        OrderBooks = new HashSet<DbOrderBook>();
    }
}

public class DbBookConfiguration : IEntityTypeConfiguration<DbBook>
{
    public void Configure(EntityTypeBuilder<DbBook> builder)
    {
        builder
            .ToTable(DbBook.TableName);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder
            .Property(x => x.ShortDescription)
            .IsRequired(false)
            .HasMaxLength(500);

        builder
            .Property(x => x.DepartmentId)
            .IsRequired(false);

        builder
            .Property(x => x.Photo)
            .IsRequired(false);

        builder
            .Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder
            .Property(x => x.IsActive)
            .IsRequired();

        builder
            .Property(x => x.PublicationDate)
            .IsRequired();

        builder
            .Property(x => x.Quantity)
            .IsRequired(false);

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .Property(x => x.CreatedBy)
            .IsRequired();

        builder
            .Property(x => x.ModifiedAt)
            .IsRequired();

        builder
            .Property(x => x.ModifiedBy)
            .IsRequired();

        builder
            .HasOne(x => x.Department)
            .WithMany(d => d.Books)
            .HasForeignKey(x => x.DepartmentId);

        builder
            .HasMany(x => x.BookAuthors)
            .WithOne(ba => ba.Book)
            .HasForeignKey(ba => ba.BookId);

        builder
            .HasMany(x => x.BookCarts)
            .WithOne(bc => bc.Book)
            .HasForeignKey(bc => bc.BookId);

        builder
            .HasMany(x => x.OrderBooks)
            .WithOne(ob => ob.Book)
            .HasForeignKey(ob => ob.BookId);
    }
}