using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFTaskRepository:ITaskRepository
    {
        private EFDbManagerContext context = new EFDbManagerContext();
        public IQueryable<Task> Tasks
        {
            get {

                var tasks = context.Tasks.Include("From").Include("To").Include("Comments").Include("CheckList").Include("Status");
                return tasks; 
            }
        }
        public IQueryable<Task> PersonTasks(Person person)
        {
            return context.Tasks.Include("From").Include("To").Include("Comments").Include("Status").Include("CheckList").Where(p => ((p.From.Email == person.Email) || (p.To.Email == person.Email)));
        }
        public int SaveTask(Task task)
        {
            if (task.TaskID == 0)
            {
                task.Time = DateTime.Now;
                context.Tasks.Add(task);
                context.Entry(task).State = System.Data.EntityState.Added;
            }
            else
                context.Entry(task).State = System.Data.EntityState.Modified;
            context.SaveChanges();
            return task.TaskID;
        }
        public void SaveComment(Comment comment)
        {
            comment.When = DateTime.Now;
            context.Comments.Add(comment);
            context.Entry(comment).State = System.Data.EntityState.Added;
            context.SaveChanges();
            context.Comments.Include("From").First(x => (x.ID == comment.ID));
        }
        public void SaveCheck(InTask check)
        {
            context.InTasks.Add(check);
            context.Entry(check).State = System.Data.EntityState.Added;
            context.SaveChanges();
            context.Tasks.Include("CheckList");
        }
        public void DeleteCheck(int checkId)
        {
            context.InTasks.Remove(context.InTasks.FirstOrDefault(x=>(x.ID == checkId)));
            context.SaveChanges();
        }
        public string GetStringStatus(int iD)
        {
            if (iD == 1)
                return "Выданно";
            return null;
        }
        public void ChangeCheckBox(int id)
        {
            InTask inTask=context.InTasks.FirstOrDefault(x => (x.ID == id));
            inTask.IsChecked = !inTask.IsChecked;
            context.Entry(inTask).State = System.Data.EntityState.Modified;
            context.SaveChanges();
            if (inTask.IsChecked) ChangeStatus(inTask.TaskID);
        }
        public void ChangeStatus(int taskID)
        {
            Task task = context.Tasks.FirstOrDefault(x=>x.TaskID==taskID);
            if (task.StatusID < 5)
            {
                if (task.StatusID == 1)
                {
                    task.StatusID = 2; ;
                    context.Entry(task).State = System.Data.EntityState.Modified;
                    context.SaveChanges();
                }
                if (task.CheckList.Where(x => x.IsChecked).Count() == 1)
                {
                    task.StatusID = 3;
                    context.Entry(task).State = System.Data.EntityState.Modified;
                    context.SaveChanges();
                }
                if (task.CheckList.Where(x => x.IsChecked).Count() == task.CheckList.Count)
                {
                    task.StatusID = 4;
                    context.Entry(task).State = System.Data.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }
        public void DoneTask(int taskId)
        {
            Task task = context.Tasks.FirstOrDefault(x => x.TaskID == taskId);
            task.StatusID = 5;
            context.Entry(task).State = System.Data.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
