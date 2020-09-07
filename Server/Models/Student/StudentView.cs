using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class StudentView
    {
        public int studentIndex { get; set; }
        public string studentName { get; set; }
        public string studentAddress { get; set; }
        public string studentIsland { get; set; }
        public string studentClass { get; set; }
        public int studentPhone { get; set; }
    }
}
