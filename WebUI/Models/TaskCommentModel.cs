using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class TaskCommentModel
    {
        public Task Task { get; set; }
        public Comment Comment { get; set; }
        public bool IsWorker { get; set; }
        public string newTask { get; set; }
    }
}