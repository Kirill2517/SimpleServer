using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Server.Extensions
{
    public static class Extensions
    {
        public static string PasswordToHash(this string password)
        {
            return SHA.GenerateSHA512String(password);
        }
    }
}
