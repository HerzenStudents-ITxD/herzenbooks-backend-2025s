using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace BookService.Models.Db;

public class DbDepartment
{
    public const string TableName = "Departments";

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [IgnoreParse]
    public ICollection<DbBook> Books { get; set; }

    public DbDepartment()
    {
        Books = new HashSet<DbBook>();
    }
}

public class DbDepartmentConfiguration : IEntityTypeConfiguration<DbDepartment>
{
    public void Configure(EntityTypeBuilder<DbDepartment> builder)
    {
        builder
            .ToTable(DbDepartment.TableName);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasMany(x => x.Books)
            .WithOne(b => b.Department)
            .HasForeignKey(b => b.DepartmentId);
    }
}