using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
  public class Write: IEntity
    {
        public int Id { get; set; }
 
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public int ReadingCount { get; set; }
        public virtual List<WriteComment> WriteComments { get; set; }
        //yeni yazı eklendiğinde bildirim olarak kullanıcılara gitsin
    }
    public class WriteComment: IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual Person Person { get; set; }
        public virtual Write Write { get; set; } 
        public bool Permission { get; set; }
        public string PersonId { get; set; }
        public int WriteId { get; set; }
        public WriteComment()
        {
            Permission = false;
        }
       
    }
}
