using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjectLayer;

namespace DataAccessLayer
{
    public class UserDL
    {
        private DBConnection dbconnection;
        public UserDL()
        {
            dbconnection = new DBConnection();
        }
        public void CreateUser(User User)
        {
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    INSERT INTO User (
                       name,
                       user_name,
                       email,
                       password
                   )
                   VALUES (
                       $name,
                       $user_name,
                       $email,
                       $password
                   )
                ";
                command.Parameters.AddWithValue("$name", User.Name);
                command.Parameters.AddWithValue("$user_name", User.UserName);
                command.Parameters.AddWithValue("$email", User.Email);
                command.Parameters.AddWithValue("$password", User.Password);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public User GetUser(string userName, string email)
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
                  FROM user where user_name=$user_name or email=$email
                ";
                command.Parameters.AddWithValue("$user_name", userName);
                command.Parameters.AddWithValue("$email", email);
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
