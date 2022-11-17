using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CommentDTO
    {
        public long ID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentTime { get; set; }
        public long UsersID { get; set; }
    }
}