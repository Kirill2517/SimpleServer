using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data
{
    public static class Constants
    {
        public const string ConnectionString = "server=127.0.0.1;uid=root;pwd=17282517qq;database=novelladatabase";

        public static class Requests
        {
            public const string GetRequest = "SELECT * FROM novelladatabase.{0};";
            public const string GetRoleOfUser = "SELECT r.* " +
                "FROM novelladatabase.{0} as s " +
                "inner join roles as r on r.roleId = s.roleId " +
                "where s.studentIndex = {1}; ";
            public const string CheckFieldString = "select EXISTS(SELECT {0} FROM {1} WHERE {0} = \"{2}\") as exist;";
            public const string CheckFieldNotString = "select EXISTS(SELECT {0} FROM {1} WHERE {0} = {2}) as exist;";
            public const string DeleteIndexString = "delete from {0} where {1} = \"{2}\"";
            public const string GetUser = "SELECT * FROM novelladatabase.{0} where login = \"{1}\" and password = \"{2}\";";
        }

        public static class StringsDataBase
        {
            public const string students = "student";
            public const string books = "books";
            public const string bookrs = "bookrs";
            public const string roles = "roles";
        }

        public static class AuthOptions
        {
            public const string ISSUER = "MyAuthServer"; // издатель токена
            public const string AUDIENCE = "MyAuthClient"; // потребитель токена
            const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
            public const int LIFETIME = 1; // время жизни токена - 1 минута
            public static SymmetricSecurityKey GetSymmetricSecurityKey()
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
            }
        }
    }
}
