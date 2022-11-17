using System;
using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class TopicDal : ITopicDAL
    {
        private string connectionString;

        public TopicDal(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<TopicDTO> GetAll()
        {
            List<TopicDTO> Topics = new List<TopicDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    comm.CommandText = "select * from Topics";
                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        TopicDTO TopicAdd = new TopicDTO();
                        TopicAdd.ID = (long)reader["ID"];
                        TopicAdd.UsersID = (long)reader["UsersID"];
                        TopicAdd.CommentID = (long)reader["CommentID"];
                        TopicAdd.Title = reader["Title"].ToString();
                        TopicAdd.Text = reader["Text"].ToString();
                        Topics.Add(TopicAdd);
                    }

                    return Topics;
                }
            }
            catch (Exception e)
            {
                TopicDTO TopicAdd = new TopicDTO();
                TopicAdd.ID = -1;
                Console.WriteLine(e.Message);
                Topics.Clear();
                Topics.Add(TopicAdd);
                return Topics;
            }
        }


        public List<TopicDTO> GetSort(string column = "Title")
        {
               List<TopicDTO> Topics = new List<TopicDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    comm.CommandText = "select * from Topics order by " + column;
                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        TopicDTO TopicAdd = new TopicDTO();
                        TopicAdd.ID = (long)reader["ID"];
                        TopicAdd.UsersID = (long)reader["UsersID"];
                        TopicAdd.CommentID = (long)reader["CommentID"];
                        TopicAdd.Title = reader["Title"].ToString();
                        TopicAdd.Text = reader["Text"].ToString();
                        Topics.Add(TopicAdd);
                    }
                    conn.Close();
                    return Topics;
                }
            }
            catch(Exception e)
            {
                TopicDTO TopicAdd = new TopicDTO();
                TopicAdd.ID = -1;
                Console.WriteLine(e.Message);
                Topics.Clear();
                Topics.Add(TopicAdd);
                return Topics;
            }
        }

        public TopicDTO Add(TopicDTO topic) //changed (void->TopicDTO)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    string query = "";
                    query = "Insert into Topics(UsersID,Title,[Text],CommentID) " +
                    "values(" + topic.UsersID.ToString() +
                    ", '" + topic.Title + "', '" + topic.Text + "'," + topic.CommentID.ToString() + ")";
                    comm.CommandText = query;
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TopicDTO topicDto = new TopicDTO();
                topicDto.ID = -1;
                return topicDto;
            }
            return topic;
        }
        public long Delete(long id)
        {
            try
            {

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "Delete from Topics where ID = @ID";
                comm.Parameters.AddWithValue("@ID", id);
                comm.ExecuteNonQuery();
                conn.Close();
                    return id;
            }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public List<TopicDTO> Find(string title)
        {
            List<TopicDTO> Topics = new List<TopicDTO>();
            try
            {

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Topics where Title Like '%" + title + "%'";
                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    TopicDTO TopicAdd = new TopicDTO();
                    TopicAdd.ID = (long)reader["ID"];
                    TopicAdd.UsersID = (long)reader["UsersID"];
                    TopicAdd.CommentID = (long)reader["CommentID"];
                    TopicAdd.Title = reader["Title"].ToString();
                    TopicAdd.Text = reader["Text"].ToString();
                    Topics.Add(TopicAdd);

                }
                conn.Close();
                return Topics;
            }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                TopicDTO TopicAdd = new TopicDTO();
                TopicAdd.ID = -1;
                Topics.Add(TopicAdd);
                return Topics;
            }
        }

    }
}