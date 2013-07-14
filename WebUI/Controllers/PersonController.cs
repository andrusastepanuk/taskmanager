using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Abstract;
using WebUI.Infrastructure;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class PersonController : Controller
    {
        private IPersonRepository repository;

        public PersonController(IPersonRepository repo)
        {
            repository = repo;
        }
        public ViewResult ShortInfo()
        {
            PersonViewModel infoPerson = null;
            try
            {
                string id = Session.SessionID;
            
            if (Session[Session.SessionID] != null)
                {
                infoPerson = new PersonViewModel() { Bar=new List<StringHelper>()};
                    infoPerson.Person = (Person)Session[Session.SessionID]; 
                    infoPerson.Bar.Add(new StringHelper() { Link = "Задания "+repository.CountNewTask(SessionManager.GetPerson()), 
                        Action = "List", Controller = "Task" });
                    infoPerson.Bar.Add(new StringHelper() { Link = "Сообщения "+repository.CountNewMessage(SessionManager.GetPerson()), 
                        Action = "List", Controller = "Message" });
                    if (SessionManager.CheckUserIsInRole("Admin"))
                        infoPerson.Bar.Add(new StringHelper() { Link = "Пользователи", 
                            Action = "List", Controller = "Admin" });
                }
            }
            catch { }
            return View(infoPerson);
        }
        public ViewResult AdminBar()
        {
            if (SessionManager.CheckUserIsInRole("Admin"))
                return View(true);
            else
                return View(false);
        }
        public ViewResult Profile()
        {
            return View(SessionManager.GetPerson());
        }
    }
}
