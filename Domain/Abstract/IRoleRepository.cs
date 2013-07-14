using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IRoleRepository
    {
        IQueryable<RoleList> Roles{get;}
        void RemoveRole(int personID, int roleID);
        void AddRole(int personID,int roleID);
    }
}
