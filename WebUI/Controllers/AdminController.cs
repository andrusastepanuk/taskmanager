using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Infrastructure;
using WebUI.Models;

namespace WebUI.Controllers
{
    [SharweAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IPersonRepository repository;
        private IRoleRepository roleRepository;
        public AdminController(IPersonRepository repo,IRoleRepository roleRep)
        {
            repository = repo;
            roleRepository = roleRep;
        }
        public ViewResult Edit(int? personId)
        {
            Person person = repository.Persons.FirstOrDefault(p => p.PersonID == personId);
            List<PersonHelper> tempD = new List<PersonHelper>();
            foreach (var role in roleRepository.Roles)
                if (person.Roles.Any(x=>x.RoleListID==role.ID))
                    tempD.Add(new PersonHelper { Key=role.ID,Value=true,Name=role.RoleName});
                else
                    tempD.Add(new PersonHelper { Key = role.ID, Value = false ,Name=role.RoleName});
            PersonEditModel pe = new PersonEditModel { Person=person,Roles=tempD};
            return View(pe);
        }
        [HttpPost]
        public ActionResult Edit(Person person,int?[] role = null)
        {
            
            if (ModelState.IsValid)
            {
                int personID = repository.SavePerson(person);

                var query = repository.Persons.First(x => x.PersonID == person.PersonID).Roles;

                for (int i = 1; i <= roleRepository.Roles.Count(); i++)
                {
                    if ((role.Any(x => x.Value == i)) && (!query.Any(x => x.RoleListID == i)))
                        roleRepository.AddRole(person.PersonID, i);
                    else
                        if (((role == null) || (!role.Any(x => x.Value == i))) && (query.Any(x => x.RoleListID == i)))
                            roleRepository.RemoveRole(person.PersonID, i);
                }

                TempData["message"] = string.Format("{0} has been saved", person.FirstName);
                //если пользователь поменял себе роли обновляеться он в сессии
                if (SessionManager.GetPerson().PersonID == person.PersonID)
                    return RedirectToAction("Change",new {controller="Admin",action="Change",personId=personID});
                else
                    return RedirectToAction("List");

            }
            else
                return View(person);
        }
        public ActionResult Change(int personId)
        {
            SessionManager.FreeSession(Session.SessionID);
            Person newPerson = repository.Persons.First(x => x.PersonID == personId);
            SessionManager.RegisterSession(Session.SessionID, newPerson);
            return RedirectToAction("List");
        }

        public ViewResult List()
        {
            Person newPerson = repository.Persons.First(x => x.PersonID == 2);
            return View(repository.Persons);
        }
        

        public ViewResult Create()
        {
            return View("Edit", new Person() { PersonID=0});
        }
        [HttpPost]
        public ActionResult Delete(int personId)
        {
            Person prod = repository.Persons.FirstOrDefault(p => p.PersonID == personId);
            if (prod != null)
            {
                repository.DeletePerson(prod);
                TempData["message"] = string.Format("{0} was deleted", prod.FirstName);
            }
            return RedirectToAction("list");
        }
    }
}
