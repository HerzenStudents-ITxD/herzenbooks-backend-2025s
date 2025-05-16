using BookService.Models.Db;
using Microsoft.EntityFrameworkCore;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.Enums;

namespace BookService.Data.Provider;
[AutoInject(InjectType.Scoped)]
public interface IDataProvider
{
    DbSet<DbBook> Books { get; set; }
    DbSet<DbBookAuthor> BookAuthors { get; set; }
    DbSet<DbAuthor> Authors { get; set; }
    DbSet<DbBookCart> BookCarts { get; set; }
    DbSet<DbOrder> Orders { get; set; }
    DbSet<DbOrderBook> OrderBooks { get; set; }
    DbSet<DbDepartment> Departments { get; set; }
    DbSet<DbEvent> Events { get; set; }

    Task SaveAsync();
    void Save();
}