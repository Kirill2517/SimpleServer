using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.Book
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string DDC { get; set; }
        public string Publisher { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public int? Year { get; set; }
        public int? Pages { get; set; }
        public string BookNumber { get; set; }
    }
}
