using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Person: IdentityUser
    { 
        public string NameSurname { get; set; }
        public DateTime RegisterDate { get; set; }

        public Gender Gender { get; set; }
 
        //public virtual List<Appointment> Appointments {get;set;}
        public Person()
        {
            RegisterDate = DateTime.Now;
        }
    }
}
