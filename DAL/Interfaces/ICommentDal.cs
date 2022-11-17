using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICommentDal
    {
        List<CommentDTO> GetAll();
        List<CommentDTO> GetSort(string column = "CommentText");
        List<CommentDTO> Find(string comment);
        CommentDTO Add(CommentDTO comment);
        void Delete(int id);
    }