using AutoMapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MapperProfiles
{
   public class TopicProfile:Profile
    {
            public TopicProfile()
            {
                CreateMap<Topic, TopicDTO>().ReverseMap();
            }
        
    }
}