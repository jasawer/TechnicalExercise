using TechnicalExercise.Controllers;
using BusinessLayer;
using BusinessObjectLayer;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace TechnicalExercise.Tests
{
    [TestClass]
    public class ApiTest
    {
        [TestMethod]
        public void GetAnimals_ReturnAllAnimals()
        {
            AnimalBL animalBL = new AnimalBL();
            var animals = animalBL.GetAnimals();
            var animalController = new AnimalController();
            var result = animalController.GetAnimals() as List<Animal>;
            Assert.IsNotNull(result);
            Assert.AreEqual(animals.Count(), result.Count);

        }

        [TestMethod]
        public void GetAnimal_ReturnAnimal()
        {
            AnimalBL animalBL = new AnimalBL();
            var animals = animalBL.GetAnimals();
            if(animals.Count()>1)
            {
                var animal=animals.First();
                var animalController = new AnimalController();
                var result = animalController.GetAnimal(animal.Id) as OkNegotiatedContentResult<Animal>;
                Assert.IsNotNull(result);
                Assert.AreEqual(animal.Id, result.Content.Id);
            }
            

        }

        [TestMethod]
        public void GetAnimal_ReturnNotFoundAnimal()
        {
            var animalController = new AnimalController();
            var result = animalController.GetAnimal(1000);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void CreateAnimal_AnimalIdCreated()
        {
            AnimalTypeBL animalTypeBL = new AnimalTypeBL();
            var animalTypes = animalTypeBL.GetAnimalTypes();
            if (animalTypes.Count() > 1)
            {
                var animalType = animalTypes.First();
                var random = new Random();
                var animal = new Animal
                {
                    Name = "Test"+random.Next(1000).ToString(),
                    Description = "Test",
                    AnimalTypeId = animalType.Id
                };
                var animalController = new AnimalController();
                var result = animalController.PostNewAnimal(animal) as OkNegotiatedContentResult<string>;
                Assert.AreEqual(result.Content, "success");
            }
            else
                Assert.Fail("Not enough parameters");
        }

        [TestMethod]
        public void UpdateAnimal_AnimalNameUpdated()
        {
            var random = new Random();
            AnimalBL animalBL = new AnimalBL();
            var animals = animalBL.GetAnimals();
            if (animals.Count() > 1)
            {
                int index = random.Next(animals.Count());
                while(animals.ToList()[index].Id==0)
                    index = random.Next(animals.Count());
                var animal = animals.ToList()[index];
                animal.Name = "test" + random.Next(1000).ToString();
                var animalController = new AnimalController();
                var result = animalController.PutAnimal(animal);
                var animal_result= animalController.GetAnimal(animal.Id) as OkNegotiatedContentResult<Animal>;
                Assert.IsNotNull(result);
                Assert.AreEqual(animal.Name, animal_result.Content.Name);
            }
        }

        [TestMethod]
        public void DeleteAnimal()
        {
            var random = new Random();
            AnimalBL animalBL = new AnimalBL();
            var animals = animalBL.GetAnimals();
            if (animals.Count() > 1)
            {
                int index = random.Next(animals.Count());
                while (animals.ToList()[index].Id == 0)
                    index = random.Next(animals.Count());
                var animal = animals.ToList()[index];
                var animalController = new AnimalController();
                var result = animalController.DeleteAnimal(animal.Id);
                var animal_result = animalController.GetAnimal(animal.Id) as OkNegotiatedContentResult<Animal>;
                Assert.IsNull(animal_result);

            }
        }

        [TestMethod]
        public void GetAnimalTypes_ReturnAllAnimalTypes()
        {
            AnimalTypeBL animalTypeBL = new AnimalTypeBL();
            var animalTypes = animalTypeBL.GetAnimalTypes();
            var animalTypeController = new AnimalTypeController();
            var result = animalTypeController.GetAnimalTypes() as List<AnimalType>;
            Assert.IsNotNull(result);
            Assert.AreEqual(animalTypes.Count(), result.Count);

        }

        [TestMethod]
        public void GetAnimalType_ReturnAnimalType()
        {
            AnimalTypeBL animalTypeBL = new AnimalTypeBL();
            var animalTypes = animalTypeBL.GetAnimalTypes();
            if (animalTypes.Count() > 1)
            {
                var animalType = animalTypes.First();
                var animalTypeController = new AnimalTypeController();
                var result = animalTypeController.GetAnimalType(animalType.Id) as OkNegotiatedContentResult<AnimalType>;
                
                Assert.AreEqual(animalType.Id, result.Content.Id);
            }
        }

        [TestMethod]
        public void GetAnimalType_ReturnNotFoundAnimalType()
        {
            var animalTypeController = new AnimalTypeController();
            var result = animalTypeController.GetAnimalType(1000);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void CreateAnimalType_AnimalTypeIdCreated()
        {
            var random = new Random();

            var animalType = new AnimalType
            {
                Name = "Test" + random.Next(1000).ToString(),
                Description = "Test"
            };
            var animalTypeController = new AnimalTypeController();
            var result = animalTypeController.PostNewAnimalType(animalType) as OkNegotiatedContentResult<string>;
            Assert.AreEqual(result.Content, "success");

        }

        [TestMethod]
        public void UpdateAnimalType_AnimalTypeNameUpdated()
        {
            var random = new Random();
            AnimalTypeBL animalTypeBL = new AnimalTypeBL();
            var animalTypes = animalTypeBL.GetAnimalTypes();
            if (animalTypes.Count() > 1)
            {
                int index = random.Next(animalTypes.Count());
                while (animalTypes.ToList()[index].Id == 0)
                    index = random.Next(animalTypes.Count());
                var animalType = animalTypes.ToList()[index];
                animalType.Name = "test"+random.Next(1000).ToString();
                var animalTypeController = new AnimalTypeController();
                var result = animalTypeController.PutAnimalType(animalType);
                var animalType_result = animalTypeController.GetAnimalType(animalType.Id) as OkNegotiatedContentResult<AnimalType>;
                Assert.IsNotNull(result);
                Assert.AreEqual(animalType.Name, animalType_result.Content.Name);
            }
        }

        [TestMethod]
        public void DeleteAnimalType()
        {
            var random = new Random();
            AnimalTypeBL animalTypeBL = new AnimalTypeBL();
            var animalTypes = animalTypeBL.GetAnimalTypes();
            if (animalTypes.Count() >= 1)
            {
                int index = random.Next(animalTypes.Count());
                while (animalTypes.ToList()[index].Id == 0)
                    index = random.Next(animalTypes.Count());
                var animalType = animalTypes.ToList()[index];
                var animalTypeController = new AnimalTypeController();
                var result = animalTypeController.DeleteAnimalType(animalType.Id);
                var animalType_result = animalTypeController.GetAnimalType(animalType.Id) as OkNegotiatedContentResult<AnimalType>;
                Assert.IsNull(animalType_result);
               
            }
        }

        [TestMethod]
        public void CreateUser_UserIdCreated()
        {
            var random = new Random();
            string index = random.Next(1000).ToString();
            var user = new User
            {
                Name = "Test" + index,
                UserName = "Test"+ index,
                Email = "Test" + index + "@email.com",
                Password= "passwordtest" + index
            };
            var userController = new UserController();
            var result = userController.PostNewUser(user) as OkNegotiatedContentResult<string>;
            Assert.AreEqual(result.Content, "success");

        }

        [TestMethod]
        public void UserLogin_UserLogged()
        {
            var login = new Login
            {
                UserName = "test_user",
                Password = "12345"
            };
            var loginController = new LoginController();
            var result = loginController.UserLogin(login) as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(result.Content);
            
        }

        [TestMethod]
        public void UserLogin_UserNotFound()
        {
            var login = new Login
            {
                UserName = "test_user____",
                Password = "12345"
            };
            var loginController = new LoginController();
            var result = loginController.UserLogin(login);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }
    }
}
