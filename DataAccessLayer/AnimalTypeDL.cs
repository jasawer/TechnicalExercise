using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjectLayer;
using System.Windows.Input;
using System.Xml.Linq;
using System.IO;

namespace DataAccessLayer
{
    public class AnimalTypeDL
    {
        private DBConnection dbconnection;
        public AnimalTypeDL()
        {
            dbconnection = new DBConnection();
        }
        public void CreateAnimalType(AnimalType AnimalType)
        {
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    INSERT INTO animal_type (
                       name,
                       description
                   )
                   VALUES (
                       $name,
                       $description
                   )
                ";
                command.Parameters.AddWithValue("$name", AnimalType.Name);
                command.Parameters.AddWithValue("$description", AnimalType.Description);

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

        public void UpdateAnimalType(AnimalType AnimalType)
        {
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    UPDATE animal_type set 
                       name=$name,
                       description=$description
                     WHERE id=$id
                ";
                command.Parameters.AddWithValue("$name", AnimalType.Name);
                command.Parameters.AddWithValue("$description", AnimalType.Description);
                command.Parameters.AddWithValue("$id", AnimalType.Id);
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

        public void DeleteAnimalType(int id)
        {
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    DELETE FROM animal_type 
                     WHERE id=$id
                ";
                command.Parameters.AddWithValue("$id", id);
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

        public IEnumerable<AnimalType> GetAnimalTypes()
        {
            List<AnimalType> animalTypes = new List<AnimalType>();
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT id,
                       name,
                       description
                  FROM animal_type
                ";

                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        AnimalType animalType = new AnimalType
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2)
                        };
                        animalTypes.Add(animalType);
                    }
                    return animalTypes;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public AnimalType GetAnimalType(int id)
        {
            AnimalType animalType = new AnimalType();
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT id,
                       name,
                       description
                  FROM animal_type where id=$id
                ";
                command.Parameters.AddWithValue("$id", id);
                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        animalType = new AnimalType
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2)
                        };
                    }
                    return animalType;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public AnimalType GetAnimalTypeByName(string name)
        {
            AnimalType animalType = new AnimalType();
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT id,
                       name,
                       description
                  FROM animal_type where name=$name
                ";
                command.Parameters.AddWithValue("$name", name);
                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        animalType = new AnimalType
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2)
                        };
                    }
                    return animalType;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}