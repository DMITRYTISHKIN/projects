using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace MVC.Models
{
    public class DataItem
    {
        public string ID;
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Автор")]
        public string Author { get; set; }
        [DisplayName("Жанр")]
        public string Genre { get; set; }
        [DisplayName("Издательство")]
        public string Publisher { get; set; }
        [DisplayName("Цена")]
        public double Price { get; set; }
    }
}