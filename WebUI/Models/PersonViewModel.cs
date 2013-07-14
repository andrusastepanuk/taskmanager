using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class PersonViewModel
    {
        public Person Person { get; set; }
        public List<StringHelper> Bar{get;set;}
    }
    public class StringHelper
    {
        public string Link { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}