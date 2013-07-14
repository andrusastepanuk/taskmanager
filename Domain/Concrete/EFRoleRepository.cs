using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Abstract;

namespace Domain.Concrete
{
    public class EFRoleRepository:IRoleRepository
    {
        private EFDbManagerContext context = new EFDbManagerContext();
        public IQueryable<RoleList> Roles
        {
            get
            {
                return context.RoleLists;
            }
        }
        public void AddRole(int personID,int roleListID)
        {
                Role role = new Role {RoleID=0, PersonID = personID, RoleListID = roleListID };
                context.Roles.Add(role);
                context.Entry(role).State = System.Data.EntityState.Added;
                context.SaveChanges();
        }
        public void RemoveRole(int personID, int roleListID)
        {
            Role role = context.Roles.FirstOrDefault(x => ((x.PersonID == personID) && (x.RoleListID == roleListID)));
                context.Roles.Remove(role);
                context.Entry(role).State = System.Data.EntityState.Deleted;
                context.Roles.Include("RoleLists");
                context.Persons.Include("Roles");    
                context.SaveChanges();
        }
    }
}
