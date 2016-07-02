using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MVC.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Список чего-то
        /// </summary>
        /// <returns></returns>
        public ActionResult List(string id)
        {
            DbSets.DatabaseModel DB = new DbSets.DatabaseModel();

            List<Models.DataItem> list = new List<Models.DataItem>();
            IOrderedQueryable<DbSets.Book> query = from item in DB.Books orderby item.Name select item;
            if(id != null)
                switch (id)
                {
                    case "name":
                        query = from item in DB.Books orderby item.Name select item;
                        break;
                    case "price":
                        query = from item in DB.Books orderby item.Price select item;
                        break;
                    case "author":
                        query = from item in DB.Books orderby item.Author select item;
                        break;
                    case "genre":
                        query = from item in DB.Books orderby item.Genre select item;
                        break;
                    case "publisher":
                        query = from item in DB.Books orderby item.Publisher select item;
                        break;
                }
            foreach(var a in query)
            {
                Models.DataItem item = new Models.DataItem()
                {
                    ID = a.ID.ToString(),
                    Name = a.Name,
                    Price = a.Price,
                    Author = a.Author,
                    Genre = a.Genre,
                    Publisher = a.Publisher
                };
                list.Add(item);
            }
            /*
            Models.DataItem item = new Models.DataItem()
            {
                ID = 100,
                Name = "Book",
                Price = 50.60
            };
            */
            return View("List", list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            DbSets.DatabaseModel DB = DbSets.DatabaseModel.Create();
            DbSets.Book book = new DbSets.Book()
            {
                ID = Guid.NewGuid(),
                Name = collection["Name"],
                Price = double.Parse(collection["Price"]),
                Author = collection["Author"],
                Genre = collection["Genre"],
                Publisher = collection["Publisher"]
            };

            DB.Books.Add(book);
            DB.SaveChanges();

            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            Guid ID = Guid.Parse(collection["ID"]);
            DbSets.DatabaseModel DB = DbSets.DatabaseModel.Create();
            //DELETE FROM BOOKS WHERE ID = <id>
            var query = from book in DB.Books where book.ID == ID select book;
            DB.Books.RemoveRange(query);
            DB.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(string id)
        {
            Guid ID = Guid.Parse(id);
            DbSets.DatabaseModel DB = DbSets.DatabaseModel.Create();
            //SELECT * 
            DbSets.Book book = DB.Books.Where(a => a.ID == ID).FirstOrDefault();
            return View(book);
        }
        [HttpPost]
        public ActionResult Change(FormCollection collection)
        {
            // Идентификатор книги
            Guid ID = Guid.Parse(collection["ID"]);
            // Устанавливается соединение с БД
            DbSets.DatabaseModel DB = DbSets.DatabaseModel.Create();
            // SELECT * FROM BOOKS WHERE ID = <id>
            DbSets.Book book = DB.Books.Where(a => a.ID == ID).FirstOrDefault();
            book.Name = collection["Name"];
                book.Price = double.Parse(collection["Price"]);
                book.Author = collection["Author"];
                book.Genre = collection["Genre"];
                book.Publisher = collection["Publisher"];

           // DB.Books.Add(book);
            DB.SaveChanges();

            return RedirectToAction("List");
        }
        public ActionResult Change(string id)
        {
            Guid ID = Guid.Parse(id);
            DbSets.DatabaseModel DB = DbSets.DatabaseModel.Create();
            //SELECT * 
            DbSets.Book book = DB.Books.Where(a => a.ID == ID).FirstOrDefault();
            return View(book);
        }
    }
}