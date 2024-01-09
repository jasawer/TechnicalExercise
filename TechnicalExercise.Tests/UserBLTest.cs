using TechnicalExercise.Controllers;
using BusinessLayer;
using BusinessObjectLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Http.Results;

namespace TechnicalExercise.Tests
{
    [TestClass]
    public class UserBLTest
    {
        [TestMethod]
        public void CreateUser_UserIdCreated()
        {
            var random = new Random();
            string index = random.Next(1000).ToString();
            var user = new User
            {
                Name = "Test" + index,
                UserName = "Test" + index,
                Email = "Test" + index + "@email.com",
                Password = "passwordtest" + index
            };
            var userBL = new UserBL();
            var result = userBL.CreateUser(user);
            Assert.AreEqual(result.Item2, "success");

        }
    }
}
