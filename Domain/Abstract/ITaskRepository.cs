using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Concrete;

namespace Domain.Abstract
{
    public interface ITaskRepository
    {
        IQueryable<Task> Tasks { get; }
        int SaveTask(Task product);
        IQueryable<Task> PersonTasks(Person person);
        void SaveComment(Comment comment);
        void SaveCheck(InTask check);
        void DeleteCheck(int checkId);
        void ChangeCheckBox(int id);
        void ChangeStatus(int taskID);
        void DoneTask(int taskId);
    }
}
