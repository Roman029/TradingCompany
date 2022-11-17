//using DAL.Concrete;
//using System.EnterpriseServices;
//using NUnit.Framework;
//using System.Configuration;
//using DTO;
//using System.Linq;
//using System.Runtime.InteropServices;

//namespace DAL.Tests
//{
  
//        [TestFixture]
//        [Transaction(TransactionOption.RequiresNew), ComVisible(true)]
//        public class UserDalTests : ServicedComponent
//        {
//            public UserDalTests()
//            { }

//            [Test]
//            public void CreateTest()
//            {
//                UserDal dal = new UserDal(ConfigurationManager.ConnectionStrings["ManagerNews"].ConnectionString);
//                var result = dal.Add(new UserDTO
//                {
//                    RoleID=1,
//                    FullName = "User from Test",
//                    Login="Login from Test",
//                    Email="Email from Test",
//                    Password="pass"
//                });
//                Assert.IsTrue(result.IDUser != 0, "returned IDUser should be more than zero");
//            }

//            [Test]
//            public void GetAllTest()
//            {
//                UserDal dal = new UserDal(ConfigurationManager.ConnectionStrings["ManagerNews"].ConnectionString);
//                var result = dal.Add(new UserDTO
//                {

//                    RoleID = 1,
//                    FullName = "User from Test",
//                    Login = "Login from Test",
//                    Email = "Email from Test",
//                    Password = "pass"

//                });
//                var users = dal.GetAll();
//                Assert.AreEqual(1, users.Count(x => x.FullName == "FullName for get all"));
//            }

//            [TearDown]
//            public void Teardown()
//            {
//                if (ContextUtil.IsInTransaction)
//                {
//                    ContextUtil.SetAbort();
//                }
//            }

//        }
//    }