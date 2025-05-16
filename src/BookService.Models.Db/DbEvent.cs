using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace BookService.Models.Db;

public class DbEvent
{
    public const string TableName = "Events";

    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? Photo { get; set; }
    public string? Link { get; set; }
    public DateTime Date { get; set; }
    public DateTime PublicationDate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Guid ModifiedBy { get; set; }
    public string Description { get; set; }

    public DbEvent()
    {
    }
}

public class DbEventConfiguration : IEntityTypeConfiguration<DbEvent>
{
    public void Configure(EntityTypeBuilder<DbEvent> builder)
    {
        builder
            .ToTable(DbEvent.TableName);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder
            .Property(x => x.Photo)
            .IsRequired(false);

        builder
            .Property(x => x.Link)
            .IsRequired(false)
            .HasMaxLength(500);

        builder
            .Property(x => x.Date)
            .IsRequired();

        builder
            .Property(x => x.PublicationDate)
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .IsRequired();

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
    }
}