using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IPersonRepository
    {
        IQueryable<Person> Persons { get; }
        int SavePerson(Person product);
        Person GetUser(string email);
        Person GetUser(string email, string password);
        IQueryable<Person> UserRoles { get; }
        Person Login(string email, string password);
        bool CheckPassword(string email, string password);
        void DeletePerson(Person person);
        string CountNewTask(Person currPerson);
        string CountNewMessage(Person currPerson);
    }
}
