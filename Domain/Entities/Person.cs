using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Person
    {
        [HiddenInput(DisplayValue = false)]
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [Required(ErrorMessage = "Укажите адресс почты")]
        [DataType(DataType.EmailAddress)]   
        public string Email { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public string Password { get; set; }//захешировать пароль
       
        public bool InRoles(string roles)
        {
            if (string.IsNullOrWhiteSpace(roles))
            {
                return false;
            }
            return Roles.Any(x => x.RoleLists.RoleName==roles);
        }
    }
}
