using System;
using DAL.Concrete;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DalTestss
{
    [TestClass]
    public class TopicDalTests
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=ManagerNews;Integrated Security=True";

        [TestMethod]
        public void GetAllTopic()
        {
            TopicDal a = new TopicDal(connectionString);
            var gl = a.GetAll();

            Assert.IsTrue(gl.Count == 0 || gl[0].ID != -1);
        }

        [TestMethod]

        public void GetSortTopic()
        {
            TopicDal a = new TopicDal(connectionString);
            var gl = a.GetSort();

            Assert.IsTrue(gl.Count >= 2 && String.Compare(gl[0].Title, gl[1].Title) < 0);
        }

        [TestMethod]

        public void FindTopic()
        {
            TopicDal a = new TopicDal(connectionString);
            var gl = a.Find("Title");

            Assert.IsTrue(gl.Count == 0 || gl[0].ID != -1);
        }
        [TestMethod]

        public void AddTopic()
        {
            TopicDal a = new TopicDal(connectionString);
            TopicDTO top = new TopicDTO();
            top.ID = -1;
            top.Title = "Title";
            top.Text = "text";
            top.CommentID = 99999999;
            var res = a.Add(top);
            Assert.IsTrue(res.ID == -1);
        }
        [TestMethod]

        public void DeleteTopic()
        {
            TopicDal a = new TopicDal(connectionString);
            var res = a.Delete(3);
            Assert.IsTrue(res != -1);


        }

    }
}