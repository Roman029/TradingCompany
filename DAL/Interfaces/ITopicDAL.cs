

using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
   public interface ITopicDAL
    {
        List<TopicDTO> GetAll();
        List<TopicDTO> GetSort(string column = "Title");
        List<TopicDTO> Find(string title);
        TopicDTO Add(TopicDTO topic); //changed void->TopicDTO
        long Delete(long id);

    }
}