using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class TaskEditModel
    {
        public Task Task { get; set; }
        public List<SelectListItem> PersonLists { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ToId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string[] mas{ get; set; }
    }
}