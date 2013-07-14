using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Abstract;
using WebUI.Models;
using WebUI.Infrastructure;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IPersonRepository repository;
        private IPostProcessor postProcessor;
        private IRoleRepository roleRepository;
        public AccountController(IPersonRepository repo,IPostProcessor post,IRoleRepository role)
        {
            repository = repo;
            postProcessor = post;
            roleRepository = role;
        }
        public ActionResult LogOut()
        {
            Session[Session.SessionID] = null;
            return Redirect(Url.Action("Index", "Home"));
        }
        public ViewResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (((String)HttpContext.Session["RandomText"]) == model.Captcha)
                {
                    Person inBasePerson = repository.GetUser(model.UserName, model.Password);
                    if (inBasePerson != null)
                    {
                        //сохранение данных пользователя в сессию
                        SessionManager.RegisterSession(Session.SessionID, inBasePerson);
                        return Redirect(returnUrl ?? Url.Action("List", "Task"));
                    }
                    else
                    {
                        //неверное имя пользователя или пароль
                        ModelState.AddModelError("", "Incorrect username or password");
                        return View();
                    }
                }
                else
                {
                    //неверное имя пользователя или пароль
                    ModelState.AddModelError("", "Incorrect captcha");
                    return View();
                }
            }
            else
                return View();
        }
        public ViewResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (((String)HttpContext.Session["RandomText"]) == model.Captcha)
                {
                    Person inBasePerson = repository.GetUser(model.Email);
                    if (inBasePerson == null)
                    {
                        Person newPerson = new Person();
                        newPerson.Email = model.Email;
                        newPerson.FirstName = (model.FirstName == null) ? "" : model.FirstName;
                        newPerson.SecondName = (model.SecondName == null) ? "" : model.SecondName;
                        newPerson.Password = model.Password;

                        //сохранение в базу
                        int personId = repository.SavePerson(newPerson);

                        roleRepository.AddRole(personId, 1);//1 это user
                        List<Role> role = new List<Role>();
                        role.Add(new Role { PersonID = personId, RoleListID = 1, RoleLists = new RoleList { ID = 1, RoleName = "User" } });
                        newPerson.Roles = role;

                        //сохранение данных пользователя в сессию
                        SessionManager.RegisterSession(Session.SessionID, newPerson);

                        postProcessor.ProcessPost(new ShippingDetails
                        {
                            EmailToAddress = model.Email,
                            Message = new StringBuilder("новый аккаунт").ToString(),
                            Title = new StringBuilder("поздравляем вы зарегистировались").ToString()
                        });
                        return Redirect(returnUrl ?? Url.Action("List", "Task"));
                    }
                    else
                    {
                        //неверное имя пользователя или пароль
                        ModelState.AddModelError("", "Пользователь с такой почтой уже существует");
                        return View();
                    }
                }
                else
                {
                    //неверное имя пользователя или пароль
                    ModelState.AddModelError("", "Incorrect captcha");
                    return View();
                }
            }
            else
                return View();
        }
    }
}
