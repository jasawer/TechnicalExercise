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
    public class AnimalDLTest
    {

        [TestMethod]
        public void CreateAnimal_AnimalIdCreated()
        {
            AnimalTypeDL animalTypeDL = new AnimalTypeDL();
            var animalTypes = animalTypeDL.GetAnimalTypes();
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
                var animalDL = new AnimalDL();
                var initAnimals = animalDL.GetAnimals();
                animalDL.CreateAnimal(animal);
                var endAnimals = animalDL.GetAnimals();
                Assert.AreNotEqual(initAnimals.Count(), endAnimals.Count());
            }
            else
                Assert.Fail("Not enough parameters");
        }

        [TestMethod]
        public void UpdateAnimal_AnimalNameUpdated()
        {
            var random = new Random();
            AnimalDL animalDL = new AnimalDL();
            var initAnimals = animalDL.GetAnimals();
            if (initAnimals.Count() > 1)
            {
                int index = random.Next(initAnimals.Count());
                while (initAnimals.ToList()[index].Id == 0)
                    index = random.Next(initAnimals.Count());
                var animal = initAnimals.ToList()[index];
                animal.Name = "test" + random.Next(1000).ToString();
                animalDL.UpdateAnimal(animal);
                var endAnimals = animalDL.GetAnimals();
                Assert.AreEqual(initAnimals.Count(), endAnimals.Count());
            }
        }

        [TestMethod]
        public void DeleteAnimal()
        {
            var random = new Random();
            AnimalDL animalDL = new AnimalDL();
            var initAnimals = animalDL.GetAnimals();
            if (initAnimals.Count() > 1)
            {
                int index = random.Next(initAnimals.Count());
                while (initAnimals.ToList()[index].Id == 0)
                    index = random.Next(initAnimals.Count());
                var animal = initAnimals.ToList()[index];
                animalDL.DeleteAnimal(animal.Id);
                var endAnimals = animalDL.GetAnimals();
                Assert.AreNotEqual(initAnimals.Count(),endAnimals.Count());

            }
        }

    }
}
