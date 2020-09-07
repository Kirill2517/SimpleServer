using Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class RoleFromRequest
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public int canAdd { get; set; }
        public int canRemove { get; set; }
    }
}
