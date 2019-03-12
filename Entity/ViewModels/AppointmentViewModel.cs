using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
  public class AppointmentViewModel
    {
        [Display(Name = "Konu")]
        public string Subject { get; set; }
        [Display(Name ="Tercih Edilen Gün")]
        public DateTime CreationDate { get; }
        [Display(Name = "Tercih Edilen Saat")]
        public DateTime RequestedTime { get; set; }
        public string PersonId { get; set; }
    }
}
