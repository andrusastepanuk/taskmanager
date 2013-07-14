using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFDbManagerContext:DbContext
    {
        public DbSet<Task> Tasks{get;set;}
        public DbSet<Person> Persons { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuss { get; set; }
        public DbSet<InTask> InTasks { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<RoleList> RoleLists { get; set; }
    }
}
