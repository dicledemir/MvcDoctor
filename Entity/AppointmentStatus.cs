using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class AppointmentStatus :IEntity
    {
        public int Id { get; set; }
        public int AppointmentID { get; set; }
        public bool Status { get; set; } 
        public DateTime Create { get; set; }
        public AppointmentStatus()
        {
            Create = DateTime.Now;
        }
    }
}
