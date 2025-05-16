using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace BookService.Models.Db;

public class DbBookAuthor
{
    public const string TableName = "BookAuthors";

    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }

    [IgnoreParse]
    public DbAuthor Author { get; set; } = null!;
    [IgnoreParse]
    public DbBook Book { get; set; } = null!;

    public DbBookAuthor()
    {
    }
}

public class DbBookAuthorConfiguration : IEntityTypeConfiguration<DbBookAuthor>
{
    public void Configure(EntityTypeBuilder<DbBookAuthor> builder)
    {
        builder
            .ToTable(DbBookAuthor.TableName);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.AuthorId)
            .IsRequired();

        builder
            .Property(x => x.BookId)
            .IsRequired();

        builder
            .HasOne(x => x.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(x => x.AuthorId);

        builder
            .HasOne(x => x.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(x => x.BookId);
    }
}