using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class StudentRegistration : ICloneable
    {
        public string studentName { get; set; }
        public string studentAddress { get; set; }
        public string studentIsland { get; set; }
        public string studentClass { get; set; }
        public int? studentPhone { get; set; }
        public string password { get; set; }
        public string login { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
