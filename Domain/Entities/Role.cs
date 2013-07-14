using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class Role
    {
        public int RoleID { get; set; }
        public int RoleListID { get; set; }
        public int PersonID { get; set; }
        public virtual RoleList RoleLists { get; set; }
    }
}
