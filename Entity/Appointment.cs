using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class Appointment
    {
        public int Id { get; set; }
        public Person person { get; set; }
        public bool Accepted { get; set; }
        public string Subject { get; set; }
        public DateTime CreationDate { get; }
        public DateTime RequestedDate { get; set; }
        public DateTime RequestedTime { get; set; }
        public Appointment()
        {
            Accepted = false;
            CreationDate = DateTime.Now;
        }
         
    }
}
