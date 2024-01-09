using TechnicalExercise.Controllers;
using BusinessLayer;
using DataAccessLayer;
using BusinessObjectLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace TechnicalExercise.Tests
{
    [TestClass]
    public class AnimalBLTest
    {

        [TestMethod]
        public void GetAnimals_ReturnAllAnimals()
        {
            AnimalBL animalBL = new AnimalBL();
            var animals = animalBL.GetAnimals();
            var animalDL = new AnimalDL();
            var result = animalDL.GetAnimals() as List<Animal>;
            Assert.IsNotNull(result);
            Assert.AreEqual(animals.Count(), result.Count);

        }

        [TestMethod]
        public void GetAnimal_ReturnAnimal()
        {
            AnimalBL animalBL = new AnimalBL();
            var animals = animalBL.GetAnimals();
            if (animals.Count() > 1)
            {
                var animal = animals.First();
                var animalDL = new AnimalDL();
                var result = animalDL.GetAnimal(animal.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(animal.Id, result.Id);
            }


        }

        [TestMethod]
        public void GetAnimal_ReturnNotFoundAnimal()
        {
            var animalDL = new AnimalDL();
            var result = animalDL.GetAnimal(1000);
            Assert.AreEqual(0,result.Id);

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
                    Name = "Test" + random.Next(1000).ToString(),
                    Description = "Test",
                    AnimalTypeId = animalType.Id
                };
                var animalBL = new AnimalBL();
                var result = animalBL.CreateAnimal(animal);
                Assert.AreEqual(result.Item2, "success");
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
                while (animals.ToList()[index].Id == 0)
                    index = random.Next(animals.Count());
                var animal = animals.ToList()[index];
                animal.Name = "test" + random.Next(1000).ToString();
                var result = animalBL.UpdateAnimal(animal);
                var animal_result = animalBL.GetAnimal(animal.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(animal.Name, animal_result.Name);
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
                var result = animalBL.DeleteAnimal(animal.Id);
                var animal_result = animalBL.GetAnimal(animal.Id);
                Assert.AreEqual("success",result.Item2);

            }
        }

    }
}
