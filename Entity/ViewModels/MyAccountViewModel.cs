using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
   public class MyAccountViewModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Appointment> AppointmentAccept { get; set; }
        public List<Appointment> AppointmentDenied { get; set; }
        public List<Appointment> AppointmentWaiting { get; set; }
    }
}
