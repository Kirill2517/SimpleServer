using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Server.Controllers
{
    [Serializable]
    public class Book
    {
        public string title { get; set; }
        public string isbn { get; set; }
        public string ddc { get; set; }
        public DateTime created { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class CalcController : ControllerBase
    {
        [HttpGet]
        [Route("allbooks")]
        public ActionResult GetAllBooks()
        {
            return Ok(JsonConvert.SerializeObject(Data.Books));
        }

        [HttpPost]
        [Route("addbook")]
        public ActionResult AddBook(Book book)
        {
            Data.Books.Add(book);
            return Ok(JsonConvert.SerializeObject(Data.Books));
        }
    }
}
