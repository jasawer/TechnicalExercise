using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BusinessLayer;
using BusinessObjectLayer;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalExercise.Tests
{
    [TestClass]
    public class AnimalTypeDLTest
    {
       

        [TestMethod]
        public void CreateAnimalType_AnimalTypeIdCreated()
        {
            AnimalTypeDL animalTypeDL = new AnimalTypeDL();
            var initAnimalTypes = animalTypeDL.GetAnimalTypes();
            if (initAnimalTypes.Count() > 1)
            {
                var random = new Random();
                var animalType = new AnimalType
                {
                    Name = "Test" + random.Next(1000).ToString(),
                    Description = "Test"
                };
                animalTypeDL.CreateAnimalType(animalType);
                var endAnimalTypes = animalTypeDL.GetAnimalTypes();
                Assert.AreNotEqual(initAnimalTypes,endAnimalTypes);
            }
            else
                Assert.Fail("Not enough parameters");
        }

        [TestMethod]
        public void UpdateAnimalType_AnimalTypeNameUpdated()
        {
            var random = new Random();
            AnimalTypeDL animalTypeDL = new AnimalTypeDL();
            var initAnimalTypes = animalTypeDL.GetAnimalTypes();
            if (initAnimalTypes.Count() > 1)
            {
                int index = random.Next(initAnimalTypes.Count());
                while (initAnimalTypes.ToList()[index].Id == 0)
                    index = random.Next(initAnimalTypes.Count());
                var animalType = initAnimalTypes.ToList()[index];
                animalType.Name = "test" + random.Next(1000).ToString();
                animalTypeDL.UpdateAnimalType(animalType);
                var endAnimalTypes = animalTypeDL.GetAnimalTypes();
                Assert.AreEqual(initAnimalTypes.Count(),endAnimalTypes.Count());
            }
        }

        [TestMethod]
        public void DeleteAnimalType()
        {
            var random = new Random();
            AnimalTypeDL animalTypeDL = new AnimalTypeDL();
            var initAnimalTypes = animalTypeDL.GetAnimalTypes();
            if (initAnimalTypes.Count() > 1)
            {
                int index = random.Next(initAnimalTypes.Count());
                while (initAnimalTypes.ToList()[index].Id == 0)
                    index = random.Next(initAnimalTypes.Count());
                var animalType = initAnimalTypes.ToList()[index];
                animalTypeDL.DeleteAnimalType(animalType.Id);
                var endAnimalTypes = animalTypeDL.GetAnimalTypes();
                Assert.AreNotEqual(initAnimalTypes.Count(),endAnimalTypes.Count());

            }
        }
    }
}
