using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
  public class Write
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public string Content { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<WriteComment> WriteComments { get; set; }
        //yeni yazı eklendiğinde bildirim olarak kullanıcılara gitsin
    }
    public class WriteComment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public virtual Person Person { get; set; }
        public virtual Write Write { get; set; } 
        public bool Permission { get; set; }
        public WriteComment()
        {
            Permission = false;
        }
       
    }
}
