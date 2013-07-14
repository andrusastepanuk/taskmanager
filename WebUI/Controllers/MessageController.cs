using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Infrastructure;
using WebUI.Models;
using System.Text;

namespace WebUI.Controllers
{
    public class MessageController : Controller
    {
        private IMessageRepository repository;
        private IPersonRepository personRepository;
        private IPostProcessor postProcessor;
        public MessageController(IMessageRepository repo,IPersonRepository personRepo,IPostProcessor post)
        {
            repository = repo;
            personRepository = personRepo;
            postProcessor = post;
        }
        public ViewResult List(int categories=1)
        {
            Person currPerson = SessionManager.GetPerson();
            if (categories == 1)
                return View(repository.Messages.Where(x=>x.ToID==currPerson.PersonID));
            else
                return View(repository.Messages.Where(x =>x.FromID == currPerson.PersonID));
        }
        public ViewResult WatchMessage(int messageId = 0)
        {
            Message mes = repository.Messages.FirstOrDefault(x => x.ID == messageId);
            Person per= SessionManager.GetPerson();
            if (per!=null)
            if ((mes.IsNew) && (mes.ToID==SessionManager.GetPerson().PersonID))
            {
                mes.IsNew=false;
                repository.ChangeNew(messageId);
            }
            return View(mes);
        }
        public ViewResult Create()
        {
            CreateMessageModel model = new CreateMessageModel() {Message = new Message { ID = 0 },PersonLists=new List<SelectListItem>() };
            foreach (var p in personRepository.Persons)
                model.PersonLists.Add(new SelectListItem { Text = p.FirstName+" "+p.SecondName, Value = p.PersonID.ToString() });
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.FromID = SessionManager.GetPerson().PersonID;
                repository.SaveMessage(message);

                postProcessor.ProcessPost(new ShippingDetails
                {
                    EmailToAddress = message.To.Email,
                    Message = "заголовок: " + message.Tittle,
                    Title = new StringBuilder("вам пришло сообщение").ToString()
                });

                TempData["message"] = string.Format("сообщение отправленно");

                return RedirectToAction("List");
            }
            else
                return View(message);
        }
        [HttpPost]
        public ActionResult Delete(int messageId)
        {
            repository.DeleteMessage(messageId);
            TempData["message"] = "сообщение удалено";
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
