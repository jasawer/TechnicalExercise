using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjectLayer;

namespace DataAccessLayer
{
    public class LoginDL
    {
        private DBConnection dbconnection;
        public LoginDL()
        {
            dbconnection = new DBConnection();
        }

        public User UserLogin(Login login)
        {
            User user = new User();
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT id,
                       name,
                       user_name,
                       email
                  FROM user where user_name=$user_name and password=$password
                ";
                command.Parameters.AddWithValue("$user_name", login.UserName);
                command.Parameters.AddWithValue("$password", login.Password);
                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        user = new User();
                        user.Id = reader.GetInt32(0);
                        user.Name = reader.GetString(1);
                        user.UserName = reader.GetString(2);
                        user.Email = reader.GetString(3);
                    }
                    return user;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
