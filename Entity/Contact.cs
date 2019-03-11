﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class Contact: IEntity
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }

        public string Email { get; set; }
        public string Message { get; set; }
    }
}
