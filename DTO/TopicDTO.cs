using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public  class TopicDTO
    {
        public long ID { get; set; }
        public long UsersID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public long CommentID { get; set; }

        
 
    }
}