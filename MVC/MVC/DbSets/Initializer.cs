using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace MVC.DbSets
{
    internal class Initializer : MigrateDatabaseToLatestVersion<DatabaseModel, MVC.Migrations.Configuration>
    {
        protected void Seed(DatabaseModel context)
        {
            Book book = new Book()
            {
                ID = Guid.NewGuid(),
                Name = "Землянин",
                Price = 99.99,
                Author = "Злотников Роман Валерьевич",
                Genre = "Боевая фантастика"
            };
            //добавление объекта в таблицу
            context.Books.Add(book);
            //Сохранить данные в БД
            context.SaveChanges();
        }
    }
}