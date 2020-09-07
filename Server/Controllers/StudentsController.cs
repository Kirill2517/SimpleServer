using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Data;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseController
    {
        [HttpGet]
        [Route("getAll")]
        public ActionResult GetAllStudent()
        {
            return Ok(JsonConvert.SerializeObject(DataBase.GetAllStudents()));
        }
    }
}
