using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public enum Status
    {
        Waiting,
        Accepted,
        Denied
        
    }
   public class Appointment: IEntity
    {
        public int Id { get; set; }
        public virtual Person person { get; set; }
        public Status Status { get; set; }
        public string Subject { get; set; }
        public DateTime CreationDate { get; }
        public DateTime RequestedDate { get; set; }
        public string PersonId { get; set; }
        public DateTime RequestedTime { get; set; }
        public short AppointmentStatus { get; set; }

        public Appointment()
        {
           
            RequestedDate = DateTime.Now;
        } 
    }
}
