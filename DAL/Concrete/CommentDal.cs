using AutoMapper;
using DAL.Interfaces;
using DTO;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace DAL.Concrete
{
    public class CommentDal : ICommentDal
    {
        private string connectionString;

        public CommentDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<CommentDTO> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Comments";
                SqlDataReader reader = comm.ExecuteReader();

                List<CommentDTO> Comments = new List<CommentDTO>();
                while (reader.Read())
                {
                    CommentDTO commentAdd = new CommentDTO();

                    commentAdd.ID = (long)reader["ID"];
                    commentAdd.UsersID = (long)reader["UsersID"];
                    commentAdd.CommentText = reader["CommentText"].ToString();
                    commentAdd.CommentTime = (DateTime)reader["CommentTime"];
                    Comments.Add(commentAdd);
                }

                return Comments;
            }
        }
        public List<CommentDTO> GetSort(string column = "CommentText")
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Comments order by " + column;
                SqlDataReader reader = comm.ExecuteReader();

                List<CommentDTO> Comments = new List<CommentDTO>();
                while (reader.Read())
                {
                    CommentDTO commentAdd = new CommentDTO();

                    commentAdd.ID = (int)reader["ID"];
                    commentAdd.CommentText = reader["CommentText"].ToString();
                    commentAdd.CommentTime = (DateTime)reader["CommentTime"];
                    Comments.Add(commentAdd);
                }
                conn.Close();
                return Comments;
            }
        }

        public CommentDTO Add(CommentDTO comment)
        { 
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    string query = "";
                    query = "Insert into Comments(CommentText,CommentTime,UsersID) " +
                    "values( '" + comment.CommentText + "', '"
                    + comment.CommentTime.Year.ToString() + "-" + comment.CommentTime.Month.ToString()
                    + "-" + comment.CommentTime.Day.ToString() + "', " + comment.UsersID.ToString() + ")";

                    Console.WriteLine(query);
                    comm.CommandText = query;
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return comment;
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "Delete from Comments where ID = @ID";
                comm.Parameters.AddWithValue("@ID", id);
                comm.ExecuteNonQuery();
                conn.Close();
            }
        }


        //може за датою? або за користувачем?
        public List<CommentDTO> Find(string comment)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Comments where CommentText Like '%" + comment + "%'";
                SqlDataReader reader = comm.ExecuteReader();

                List<CommentDTO> Comments = new List<CommentDTO>();
                while (reader.Read())
                {
                    CommentDTO commentAdd = new CommentDTO();

                    commentAdd.ID = (long)reader["ID"];
                    commentAdd.CommentText = reader["CommentText"].ToString();
                    commentAdd.CommentTime = (DateTime)reader["CommentTime"];
                    Comments.Add(commentAdd);

                }
                conn.Close();
                return Comments;
            }
        }

    }

}