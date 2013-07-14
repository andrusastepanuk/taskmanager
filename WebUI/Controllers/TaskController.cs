using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;
using WebUI.Infrastructure;

namespace WebUI.Controllers
{
    
    public class TaskController : Controller
    {
        private ITaskRepository repository;
        private IPersonRepository personRepository;

        public TaskController(ITaskRepository repo,IPersonRepository personRepo)
        {
            repository = repo;
            personRepository = personRepo;
        }
        [SharweAuthorize(Roles = "User")]
        public ViewResult WatchTask(int taskId=0)
        {
            bool isWorker = (repository.Tasks.First(x => x.TaskID == taskId).ToID ==
                ((Person)Session[Session.SessionID]).PersonID) ? true : false;
            Task task = repository.Tasks.First(x=>x.TaskID==taskId);
            if ((task.StatusID == 1) && (isWorker)) { repository.ChangeStatus(taskId); }
            return View(new TaskCommentModel(){Task=task,Comment = new Comment(),IsWorker=isWorker});
        }

        [HttpPost]
        public PartialViewResult WatchTask(int taskId,TaskCommentModel taskCommentodel)
        {
            if (ModelState.IsValid)
            {
                Comment tempComment = taskCommentodel.Comment;
                tempComment.ID = 0;
                tempComment.FromID = ((Person)System.Web.HttpContext.Current.Session[
                System.Web.HttpContext.Current.Session.SessionID]).PersonID;//эта зверюга возвращает ID пользователя                    
                tempComment.TaskID = taskId;
                repository.SaveComment(tempComment);
            }
            return PartialView("testPartial", repository.Tasks.First(x => x.TaskID == taskId).Comments.OrderBy(x=>x.When));
        }

        [HttpPost]
        public PartialViewResult Check(int taskId, TaskCommentModel taskCommentodel)
        {
            if (ModelState.IsValid)
            {
                InTask tempInTask = new InTask();
                tempInTask.CheckTask = taskCommentodel.newTask;
                tempInTask.TaskID = taskId;
                repository.SaveCheck(tempInTask);
            }
            return PartialView("taskPartial", repository.Tasks.First(x => x.TaskID == taskId).CheckList.OrderBy(x => x.ID));
        }

        public ViewResult List()
        {
            if (Session[Session.SessionID] != null)
            {
                TaskViewModel tvm = new TaskViewModel();
                Person currentPerson = (Person)Session[Session.SessionID];
                
                tvm.Tasks = repository.PersonTasks(currentPerson);

                tvm.isManager=currentPerson.InRoles("Manager");

                tvm.Categorize = new List<string>();
                //пока нет навигации заданий
                //tvm.Categorize = tvm.Tasks.GroupBy(x => x.From.Email)
                //    .Select(x=>x.FirstOrDefault())
                //    .Select(x=>x.From.FirstName)
                //    .OrderBy(p => p);
                
                return View(tvm);
            }
            else
                return View();
        }

        //создание задания
        [SharweAuthorize(Roles = "Manager")]
        public ViewResult Edit(int? taskId)
        {
            TaskEditModel task = new TaskEditModel() { Task = repository.Tasks
                .FirstOrDefault(p => p.TaskID == taskId),
                PersonLists=new List<SelectListItem>() };
            foreach (var p in personRepository.Persons)
                task.PersonLists.Add(new SelectListItem { Text = p.FirstName+" "+p.SecondName, Value = p.PersonID.ToString() });
            return View(task);
        }
        [SharweAuthorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Edit(Task task, int Toid = 0,string[] mas=null)//TaskEditModel task
        {
            if (ModelState.IsValid)
            {
                task.FromID = SessionManager.GetPerson().PersonID;
                task.ToID = Toid;
                int k = repository.SaveTask(task);
                foreach (var m in mas)
                    if (m!="")
                        repository.SaveCheck(new InTask { CheckTask = m, TaskID = k });
                TempData["message"] = string.Format("Task has been change");
                return RedirectToAction("List");
            }
            else
                return View(task);
        }
        [SharweAuthorize(Roles = "Manager")]
        public ViewResult Create()
        {
            TaskEditModel task = new TaskEditModel(){Task = new Task{TaskID=0 ,StatusID=1},
                PersonLists=new List<SelectListItem>()};
            foreach (var p in personRepository.Persons)
                task.PersonLists.Add(new SelectListItem { Text = p.FirstName, 
                    Value = p.PersonID.ToString() });
            return View("Edit", task);
        }
        [HttpPost]
        public ActionResult DeleteCheck(int checkId)
        {
            repository.DeleteCheck(checkId);
            return RedirectToAction("list");
        }
        [HttpPost]
        public ActionResult Done(int taskId)
        {
            repository.DoneTask(taskId);
            return RedirectToAction("list");
        }
        [HttpPost]
public ActionResult Checked(string isChecked)
{
    int id;
    if (int.TryParse(isChecked,out id))
    {
        repository.ChangeCheckBox(id);
    }
    return null;
}

    }
}
