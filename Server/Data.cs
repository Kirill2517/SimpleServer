using Server.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public static class Data
    {
        public static List<Book> Books { get; set; } = new List<Book>();

        static Data()
        {
            Books.Add(new Book() { created = DateTime.Now, ddc = "123ddclms", isbn = "9951", title = "bookour1" });
            Task.Delay(500);
            Books.Add(new Book() { created = DateTime.Now, ddc = "ddc2", isbn = "isbn2", title = "bookour2" });
        }


        //some stuff
    }
}
