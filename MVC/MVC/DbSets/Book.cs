using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MVC.DbSets
{
    
    public class Book
    {
        [Key()]
        public Guid ID { get; set; }
        [StringLength(255)]
        [Required()]
        public string Name { get; set; }
        [StringLength(255)]
        [Required()]
        public string Author { get; set; }
        [StringLength(255)]
        [Required()]
        public string Genre { get; set; }
        [StringLength(255)]
        [Required()]
        public string Publisher { get; set; }
        [Required()]
        public double Price { get; set; }
    }
}