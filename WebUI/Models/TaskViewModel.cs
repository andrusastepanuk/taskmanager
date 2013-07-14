using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class TaskViewModel
    {
        public IQueryable<Task> Tasks { get; set; }
        public IEnumerable<string> Categorize { get; set; }
        public bool isManager { get; set; }
    }
}