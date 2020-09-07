using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Models.Book;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController
    {
        [HttpGet]
        [Route("getAll")]
        public ActionResult GetAllStudent()
        {
            return Ok(DataBase.GetAllBooks());
        }

        [HttpPost]
        [Route("addBook")]
        [Authorize]
        public ActionResult AddBook(Book book)
        {
            if (book.ISBN is null || book.Title is null)
                return BadRequest(new { errorText = "indexes can not be null" });
            var role = DataBase.GetRoleOfUser(studentId);
            if (role.canAdd)
            {
                if (DataBase.AddBook(book))
                    return Ok(book);
                return BadRequest(new { errorText = "ISBN exists" });
            }
            else
                return BadRequest(new { errorText = "You have no access." });
        }

        [HttpDelete]
        [Route("deleteBook/{isbn}")]
        [Authorize]
        public ActionResult DeleteBook(string isbn)
        {
            if (isbn is null)
                return BadRequest(new { errorText = "isbn can not be null" });
            var role = DataBase.GetRoleOfUser(studentId);
            if (role.canRemove)
            {
                if (DataBase.BookISBNIxists(isbn))
                    if (DataBase.removeBook(isbn))
                        return Ok(new { response = "book was deleted" });
                return BadRequest(new { errorText = "ISBN doesn't exist" });
            }
            else
                return BadRequest(new { errorText = "You have no access." });
        }
    }
}
