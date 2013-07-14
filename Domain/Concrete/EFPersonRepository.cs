using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFPersonRepository:IPersonRepository
    {
        private EFDbManagerContext context = new EFDbManagerContext();
        public IQueryable<Person> Persons
        {
            get
            {
                context.Roles.Include("RoleLists");
                return context.Persons.Include("Roles");
            }
        }
        public IQueryable<Person> UserRoles
        {
            get
            {
                context.Roles.Include("RoleLists");
                return context.Persons.Include("Roles");
            }
        }
        public int SavePerson(Person person)
        {
            if (person.PersonID == 0)
            {
                context.Persons.Add(person);
                context.Entry(person).State = System.Data.EntityState.Added;
            }
            else
                context.Entry(person).State = System.Data.EntityState.Modified;
            context.SaveChanges();
            return person.PersonID;
        }

        public Person GetUser(string email)
        {
            return context.Persons.FirstOrDefault(p => string.Compare(p.Email, email, true) == 0);
        }

        public Person GetUser(string email, string password)
        {
            return context.Persons.FirstOrDefault(p => ((string.Compare(p.Email, email, true) == 0)
                && (string.Compare(p.Password, password, true) == 0)));
        }

        public bool CheckPassword(string email,string password)
        {
            return context.Persons.Any(p => ((string.Compare(p.Email, email, true) == 0)
                && (string.Compare(p.Password, password, true) == 0)));
        }
        
        public Person Login(string email, string password)
        {
            return context.Persons.FirstOrDefault(p => string.Compare(p.Email, email, true) == 0 && p.Password == password);
        }

        public void DeletePerson(Person person)
        {
            context.Persons.Remove(person);
            context.SaveChanges();
        }
        public string CountNewMessage(Person currPerson)
        {
            int newMess = context.Messages.Where(x => (x.ToID == currPerson.PersonID) && (x.IsNew)).Count();
            if (newMess == 0)
                return "";
            else
                return "(" + newMess + ")";
        }
        public string CountNewTask(Person currPerson)
        {
            int newTask = context.Tasks.Where(x => (x.ToID == currPerson.PersonID) && (x.StatusID==1)).Count();
            if (newTask == 0)
                return "";
            else
                return "(" + newTask + ")";
        }
    }
}
