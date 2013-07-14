using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class InTask
    {
        public int TaskID { get; set; }
        public string CheckTask { get; set; }
        public bool IsChecked { get; set; }
        public int ID { get; set; }
    }
}
