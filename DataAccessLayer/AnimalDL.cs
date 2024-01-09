using System.Collections.Generic;
using System;
using System.Data.SQLite;
using BusinessObjectLayer;



namespace DataAccessLayer
{
    public class AnimalDL
    {
        private DBConnection dbconnection;
        public AnimalDL()
        {
            dbconnection = new DBConnection();
        }
        public void CreateAnimal(Animal animal)
        {
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    INSERT INTO animal (
                       name,
                       description,
                       birth_date,
                       animal_type_id
                   )
                   VALUES (
                       $name,
                       $description,
                       $birth_date,
                       $animal_type_id
                   )
                ";
                command.Parameters.AddWithValue("$name", animal.Name);
                command.Parameters.AddWithValue("$description", animal.Description);
                command.Parameters.AddWithValue("$birth_date", animal.BirthDate);
                command.Parameters.AddWithValue("$animal_type_id", animal.AnimalTypeId);
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

        public void UpdateAnimal(Animal animal)
        {
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    UPDATE animal set 
                       name=$name,
                       description=$description,
                       birth_date=$birth_date,
                       animal_type_id=$animal_type_id
                     WHERE id=$id
                ";
                command.Parameters.AddWithValue("$name", animal.Name);
                command.Parameters.AddWithValue("$description", animal.Description);
                command.Parameters.AddWithValue("$birth_date", animal.BirthDate);
                command.Parameters.AddWithValue("$animal_type_id", animal.AnimalTypeId);
                command.Parameters.AddWithValue("$id", animal.Id);
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

        public void DeleteAnimal(int id)
        {
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    DELETE FROM animal 
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

        public IEnumerable<Animal> GetAnimals()
        {
            List<Animal> animals = new List<Animal>();
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT id,
                       name,
                       description,
                       birth_date,
                       animal_type_id
                  FROM animal
                ";

                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Animal animal = new Animal
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            BirthDate = reader.GetDateTime(3)
                        };
                        animals.Add(animal);
                    }
                    return animals;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public Animal GetAnimal(int id)
        {
            Animal animal = new Animal();
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT id,
                       name,
                       description,
                       birth_date,
                       animal_type_id
                  FROM animal where id=$id
                ";
                command.Parameters.AddWithValue("$id", id);
                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        animal = new Animal
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            BirthDate = reader.GetDateTime(3)
                        };
                        //animal.AnimalType = reader.GetInt16("birth_date");
                    }
                    return animal;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Animal GetAnimalByName(string name)
        {
            Animal animal = new Animal();
            using (var connection = new SQLiteConnection(dbconnection.DBsource()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                    SELECT id,
                       name,
                       description,
                       birth_date,
                       animal_type_id
                  FROM animal where name=$name
                ";
                command.Parameters.AddWithValue("$name", name);
                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        animal = new Animal
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            BirthDate = reader.GetDateTime(3)
                        };
                        //animal.AnimalType = reader.GetInt16("birth_date");
                    }
                    return animal;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}