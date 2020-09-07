using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Server.Data;
using Server.Extensions;
using Server.Models;
using static Server.Data.Constants;
namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        [HttpPost("token")]
        public ActionResult Token(StudentLogin student)
        {
            var identity = GetIdentity(student.login, student.password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                id = int.Parse(identity.FindFirst("studentId").Value)
            };

            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpPost("registration")]
        public ActionResult Registration(StudentRegistration student)
        {
            if (student.login is null || student.password is null || student.studentName is null)
                return BadRequest(new { errorText = "Indexes can not be null!" });

            if (DataBase.CheckUserData(student))
                return BadRequest(new { errorText = "User exists" });

            DataBase.Registration(student);
            return Token(new StudentLogin() { login = student.login, password = student.password });
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            StudentView person = DataBase.GetStudentOrDefault(login, password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.studentName),
                    new Claim("studentId", person.studentIndex.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
