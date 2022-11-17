using DAL.Interfaces;
using DAL.Concrete;
using DTO;
using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleTrad
{
    public class Program
    {

        static private UserDal userDal;
        static private TopicDal topicDal;
        static private CommentDal commentDal;

        static private UserDTO currentUser;
        static uint TryCount = 3;
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;

            //string connStr = ConfigurationManager.ConnectionStrings["ManagerNews"].ConnectionString;

            string connectionString = "Data Source=localhost;Initial Catalog=ManagerNews;Integrated Security=True";
            topicDal = new TopicDal(connectionString);
            userDal = new UserDal(connectionString);
            commentDal = new CommentDal(connectionString);

            uint isLogIn = LogIn();

            while (isLogIn != 0)
            {
                //Console.Clear();
                Console.WriteLine("\n\n What do you want to DO?");
                Console.WriteLine("\ntype 'l' to get List of entities");
                Console.WriteLine("type 's' to Sort entity");
                Console.WriteLine("type 'f' to Find entity");
                if (isLogIn == 2)
                {
                    Console.WriteLine("type 'a' to Add entity");
                    Console.WriteLine("type 'r' to Remove entity");
                }
                //...
                Console.WriteLine("type 'o' to logOut");
                Console.WriteLine("type 'q' to Quit");
                try
                {

                    char c = char.Parse(Console.ReadLine());

                    switch (c)
                    {
                        case 'l': //show list of entities
                            {
                                Console.WriteLine("\nList of all entity:");
                                PrintAll();
                            }
                            break;
                        case 's': //
                            {
                                Console.WriteLine("\nSorted by Title:");
                                PrintSorted();
                            }
                            break;
                        case 'a': //create new entity
                            {
                                Console.WriteLine("Add:");
                                AddUser();
                            }
                            break;
                        case 'r': //remove entity
                            {
                                Console.WriteLine("If you are the Owner or Administrator you can delete the user.\nEnter ID:");
                                if (isLogIn == 2)
                                {
                                    int id = int.Parse(Console.ReadLine());
                                    topicDal.Delete(id);
                                }
                                else
                                {
                                    throw new Exception("Not permission");
                                }
                            }
                            break;

                        case 'f':
                            {
                                Console.WriteLine("Please, enter a word or letters or a price: "); // find by Title
                                string ttl = Console.ReadLine().ToString();
                                Find(ttl);
                            }
                            break;
                        case 'o':
                            {
                                isLogIn = LogIn();
                            }
                            break;
                        case 'q':
                            return;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static private uint LogIn()
        {
            uint isLogIn = 0;

            for (int i = 0; i < TryCount && isLogIn == 0; i++)
            {
                currentUser = new UserDTO();
                Console.Write("Please enter your login: ");
                currentUser.Login = Console.ReadLine().ToString();

                Console.Write("\nPlease enter your password: ");
                currentUser.Password = Console.ReadLine().ToString();

                isLogIn = userDal.LogIn(currentUser);
                Console.WriteLine("user: " + isLogIn.ToString());
            }
            return isLogIn;
        }

        static void Find(string str)
        {
            PrintJoinAll(topicDal.Find(str));
        }
        static void PrintAll()
        {
            var topics = topicDal.GetAll();
            PrintJoinAll(topics);
        }
        static void PrintSorted()
        {

            var topics = topicDal.GetSort();
            PrintJoinAll(topics);
        }
        static void PrintJoinAll(List<TopicDTO> topics)
        {
            var users = userDal.GetAll();
            var comments = commentDal.GetAll();

            var res = from ts in topics
                      join us in users on ts.UsersID equals us.IDUser
                      join cs in comments on ts.CommentID equals cs.ID
                      select new { ID = ts.ID, FullName = us.FullName, Title = ts.Title, Text = ts.Text, Comment = cs.CommentText };
            foreach (var i in res)
            {
                Console.WriteLine($"{i.ID} {i.FullName} \nTitle: {i.Title} \nText: {i.Text}");
            }
        }
        static private void AddUser()
        {
            TopicDTO topic = new TopicDTO();

            topic.UsersID = currentUser.IDUser;

            Console.Write("Please enter Title: ");
            topic.Title = Console.ReadLine().ToString();

            Console.Write("Please enter Text:");
            topic.Text = Console.ReadLine().ToString();

            CommentDTO comment = new CommentDTO();

            Console.Write("Please enter Comment:");
            comment.CommentText = Console.ReadLine().ToString();
            comment.UsersID = currentUser.IDUser;
            comment.CommentTime = DateTime.Now;

            commentDal.Add(comment);

            var comms= commentDal.Find(comment.CommentText);
            if ( comms.Count==0)
            {
                Console.WriteLine("error!");
            }
            else
            {
              
                topic.CommentID = comms[0].ID;
                Console.WriteLine(topic.CommentID.ToString());
                topicDal.Add(topic);

            }

        }


    }
}