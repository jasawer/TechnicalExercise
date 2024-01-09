using BusinessObjectLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserBL
    {
        private UserDL userDL;
        public UserBL()
        {
            userDL = new UserDL();
        }
        public Tuple<bool, string> CreateUser(User user)
        {
            if (GetUser(user.UserName, user.Email).Id == 0)
            {
                userDL.CreateUser(user);
                return Tuple.Create(true, "success");
            }
            else
                return Tuple.Create(false, "duplicate");

        }
        public User GetUser(string userName, string email)
        {
            return userDL.GetUser(userName, email);
        }
    }
}
