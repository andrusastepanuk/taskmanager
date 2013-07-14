using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;
using System.Web.Mvc;

namespace WebUI.Models
{
    public class PersonEditModel
    {
        public Person Person { get; set; }
        public IEnumerable<PersonHelper> Roles { get; set; }
    }
    public class PersonHelper
    {
        public int Key { get; set; }
        public bool Value { get; set; }
        public string Name { get; set; }
    }
}