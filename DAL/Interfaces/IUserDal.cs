using System;
using DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserDal
    {
        List<UserDTO> GetAll();
        List<UserDTO> Find(string fullName);
        List<UserDTO> GetSort(string column = "FullName");
        UserDTO Add(UserDTO user);//changed
        void Delete(int id);
    }
}