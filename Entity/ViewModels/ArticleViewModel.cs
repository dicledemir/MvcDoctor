using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
  public  class ArticleViewModel
    {
        public virtual List<Category> Categories { get; set; }
        public virtual List<Write> Writes { get; set; }
    }
}
