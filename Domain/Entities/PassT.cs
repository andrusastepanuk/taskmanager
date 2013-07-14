using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    //пока что в самом пользователе
    public class PassT
    {
        public int PersonID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
