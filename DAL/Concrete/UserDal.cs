using System;
using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class UserDal : IUserDal
    {
        private string connectionString;

        public UserDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<UserDTO> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Users";
                SqlDataReader reader = comm.ExecuteReader();

                List<UserDTO> users = new List<UserDTO>();
                while (reader.Read())
                {
                    UserDTO userAdd = new UserDTO();

                    userAdd.IDUser = (long)reader["IDUser"];
                    userAdd.FullName = reader["FullName"].ToString();
                    userAdd.Login = reader["Login"].ToString();
                    userAdd.Email = reader["Email"].ToString();
                    //userAdd.Password = (byte[])reader["Password"];
                    users.Add(userAdd);
                }

                return users;
            }
        }
        public List<UserDTO> GetSort(string column = "FullName")
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Users order by " + column;
                SqlDataReader reader = comm.ExecuteReader();

                List<UserDTO> users = new List<UserDTO>();
                while (reader.Read())
                {
                    UserDTO userAdd = new UserDTO();

                    userAdd.IDUser = (long)reader["IDUser"];
                    userAdd.FullName = reader["FullName"].ToString();
                    userAdd.Login = reader["Login"].ToString();
                    userAdd.Email = reader["Email"].ToString();
                    userAdd.Password = reader["Password"].ToString();
                    users.Add(userAdd);
                }
                conn.Close();
                return users;
            }
        }

        public UserDTO Add(UserDTO user)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    string query = "";
                    query = "Insert into Users(RoleID,FullName,[Login],Email,[Password]) " +
                    "values(" + user.RoleID.ToString() +
                    ", '" + user.FullName + "', '" + user.Login +
                    "', '" + user.Email + "', ";
                    foreach (var i in user.Password)
                    {
                        query += i.ToString();
                    }
                    query += ")";
                    comm.CommandText = query;
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return user;
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "Delete from Users where IDUser = @IDUser";
                comm.Parameters.AddWithValue("@IDUser", id);
                comm.ExecuteNonQuery();
                conn.Close();
            }
        }
        public List<UserDTO> Find(string fullName)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Users where FullName Like '" + fullName + "%'";
                SqlDataReader reader = comm.ExecuteReader();

                List<UserDTO> users = new List<UserDTO>();
                while (reader.Read())
                {
                    UserDTO userAdd = new UserDTO();

                    userAdd.IDUser = (long)reader["IDUser"];
                    userAdd.FullName = reader["FullName"].ToString();
                    userAdd.Login = reader["Login"].ToString();
                    userAdd.Email = reader["Email"].ToString();
                    userAdd.Password = reader["Password"].ToString();
                    users.Add(userAdd);
                }
                conn.Close();
                return users;
            }
        }

        public uint LogIn(UserDTO user)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                SqlDataReader reader;
                try
                {
                    conn.Open();
                    comm.CommandText = "select *, Roles.RoleName from Users" + " join Roles on Users.RoleID = Roles.RoleID" + " where cast(Users.Password as int) = " + user.Password + " And Login like " +user.Login;
                    reader = comm.ExecuteReader();
                    List<UserDTO> users = new List<UserDTO>();
                    if (reader.Read())
                    {
                        user.FullName = reader["FullName"].ToString();
                        user.IDUser = (long)reader["IDUser"];
                        user.Email = reader["Email"].ToString();

                        if (reader["RoleName"].ToString() == "client")
                        {
                            return 1;
                        }
                        return 2;
                    }
                    return 0;
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }

            }
        }
    }

}