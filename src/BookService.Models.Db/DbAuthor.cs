using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace BookService.Models.Db;

public class DbAuthor
{
    public const string TableName = "Authors";

    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronim { get; set; }

    [IgnoreParse]
    public ICollection<DbBookAuthor> BookAuthors { get; set; }

    public DbAuthor()
    {
        BookAuthors = new HashSet<DbBookAuthor>();
    }
}

public class DbAuthorConfiguration : IEntityTypeConfiguration<DbAuthor>
{
    public void Configure(EntityTypeBuilder<DbAuthor> builder)
    {
        builder
            .ToTable(DbAuthor.TableName);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Patronim)
            .IsRequired(false)
            .HasMaxLength(100);

        builder
            .HasMany(x => x.BookAuthors)
            .WithOne(ba => ba.Author)
            .HasForeignKey(ba => ba.AuthorId);
    }
}