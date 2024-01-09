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
    public class AnimalTypeBLTest
    {
        [TestMethod]
        public void GetAnimalTypes_ReturnAllAnimalTypes()
        {
            AnimalTypeBL animalTypeBL = new AnimalTypeBL();
            var animalTypes = animalTypeBL.GetAnimalTypes();
            var animalTypeDL = new AnimalTypeDL();
            var result = animalTypeDL.GetAnimalTypes() as List<AnimalType>;
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
                var animalTypeDL = new AnimalTypeDL();
                var result = animalTypeDL.GetAnimalType(animalType.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(animalType.Id, result.Id);
            }


        }

        [TestMethod]
        public void GetAnimalType_ReturnNotFoundAnimalType()
        {
            var animalTypeDL = new AnimalTypeDL();
            var result = animalTypeDL.GetAnimalType(1000);
            Assert.AreEqual(0, result.Id);

        }

        [TestMethod]
        public void CreateAnimalType_AnimalTypeIdCreated()
        {
            AnimalTypeBL animalTypeBL = new AnimalTypeBL();
            var animalTypes = animalTypeBL.GetAnimalTypes();
            if (animalTypes.Count() > 1)
            {
                var animalTypeType = animalTypes.First();
                var random = new Random();
                var animalType = new AnimalType
                {
                    Name = "Test" + random.Next(1000).ToString(),
                    Description = "Test"
                };
                var result = animalTypeBL.CreateAnimalType(animalType);
                Assert.AreEqual(result.Item2, "success");
            }
            else
                Assert.Fail("Not enough parameters");
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
                animalType.Name = "test" + random.Next(1000).ToString();
                var result = animalTypeBL.UpdateAnimalType(animalType);
                var animalType_result = animalTypeBL.GetAnimalType(animalType.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(animalType.Name, animalType_result.Name);
            }
        }

        [TestMethod]
        public void DeleteAnimalType()
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
                var result = animalTypeBL.DeleteAnimalType(animalType.Id);
                var animalType_result = animalTypeBL.GetAnimalType(animalType.Id);
                Assert.AreEqual("success", result.Item2);

            }
        }
    }
}
