using TechnicalExercise.Controllers;
using BusinessLayer;
using BusinessObjectLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Http.Results;
using DataAccessLayer;

namespace TechnicalExercise.Tests
{
    [TestClass]
    public class UserDLTest
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
            
            var userDL = new UserDL();
            userDL.CreateUser(user);
            var newUser=userDL.GetUser(user.UserName, user.Email);
            Assert.AreEqual(user.UserName,newUser.UserName);
            Assert.AreEqual(user.Email,newUser.Email);

        }
    }
}
