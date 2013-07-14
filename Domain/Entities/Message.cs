using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Message
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int FromID { get; set; }//
        [ForeignKey("FromID")]
        public virtual Person From { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ToID { get; set; }//
        [ForeignKey("ToID")]
        public virtual Person To { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime When { get; set; }
        public string Tittle { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool IsNew { get; set; }
    }
}
