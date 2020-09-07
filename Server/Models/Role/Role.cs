using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Role
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public bool canAdd { get; set; }
        public bool canRemove { get; set; }

        public Role(RoleFromRequest role)
        {
            this.roleId = role.roleId;
            this.roleName = role.roleName;
            this.canAdd = Convert.ToBoolean(role.canAdd);
            this.canRemove = Convert.ToBoolean(role.canRemove);
        }
    }
}
