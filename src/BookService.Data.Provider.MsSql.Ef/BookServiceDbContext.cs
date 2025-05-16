using BookService.Data.Provider;
using BookService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookService.Data.Provider.MsSql.Ef;

public class BookServiceDbContext : DbContext, IDataProvider
{
    public BookServiceDbContext(DbContextOptions<BookServiceDbContext> options)
        : base(options)
    {
    }

    public DbSet<DbBook> Books { get; set; }
    public DbSet<DbBookAuthor> BookAuthors { get; set; }
    public DbSet<DbAuthor> Authors { get; set; }
    public DbSet<DbBookCart> BookCarts { get; set; }
    public DbSet<DbOrder> Orders { get; set; }
    public DbSet<DbOrderBook> OrderBooks { get; set; }
    public DbSet<DbDepartment> Departments { get; set; }
    public DbSet<DbEvent> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("UniversityHelper.BookService.Models.Db"));
    }

    public async Task SaveAsync()
    {
        await SaveChangesAsync();
    }

    public void Save()
    {
        SaveChanges();
    }
}