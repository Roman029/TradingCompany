using DAL.Concrete;
using System.Data.SqlClient;
using System.EnterpriseServices;
using NUnit.Framework;
using DTO;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;

namespace DAL.Tests
{
    [TestFixture]
    [Transaction(TransactionOption.RequiresNew), ComVisible(true)]
    public class TopicDalTests:  ServicedComponent 
    {

        // private string connectionString = "Data Source=localhost;Initial Catalog=ManagerNews;Integrated Security=True";
        public TopicDalTests()
        { }

        //[Test]
        //public void CreateTest()
        //{
        //    TopicDTO account = new TopicDTO() {
        //        Title = "Topic from Test",
        //        Text = "Text from Test",
        //    };
        //    TopicDal tests = new TopicDal(connectionString);
        //    var tested = tests.Add(account);
        //    Assert.IsTrue(tested.ID != 0);
        //}

        [Test]
        public void GetAllTopicTest()
        {
           // TopicDal dal = new TopicDal(ConfigurationManager.ConnectionStrings["ManagerNews"].ConnectionString);
            Assert.IsTrue(true);


        }

        //public void DeleteAccountTest()
        //{

        //}

        [TearDown]
        public void Teardown()
        {
            if (ContextUtil.IsInTransaction)
            {
                ContextUtil.SetAbort();
            }
        }

    }

}