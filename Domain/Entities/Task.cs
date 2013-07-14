using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Task
    {
        [HiddenInput(DisplayValue = false)]
        public int TaskID { get; set; }
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int FromID { get; set; }//
        [ForeignKey("FromID")]
        public virtual Person From{ get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ToID { get; set; }//
        [ForeignKey("ToID")]
        public virtual Person To { get; set; }
        [Display(Name = "Название задания")]
        public string Title { get; set; }
        [Display(Name = "Описание задания")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public virtual ICollection<InTask> CheckList { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int StatusID { get; set; }
        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; }
        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        public DateTime Time { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
