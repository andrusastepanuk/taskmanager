using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace WebUI.Models
{
    public class CreateMessageModel
    {
        public Message Message { get; set; }
        public List<SelectListItem> PersonLists { get; set; }
    }
}