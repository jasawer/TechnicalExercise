using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjectLayer;
using DataAccessLayer;

namespace BusinessLayer
{
    public class AnimalBL
    {
        private AnimalDL animalDL;
        private AnimalTypeBL animalTypeBL;
        public AnimalBL()
        {
            animalDL = new AnimalDL();
            animalTypeBL = new AnimalTypeBL();
        }
        public Tuple<bool, string> CreateAnimal(Animal animal)
        {
            if (animalTypeBL.GetAnimalType(animal.AnimalTypeId).Id == 0)
                return Tuple.Create(false, "wrong data");
            else
                if (GetAnimalByName(animal.Name).Id == 0)
            {
                animalDL.CreateAnimal(animal);
                return Tuple.Create(true, "success");
            }
            else
                return Tuple.Create(false, "duplicated");
        }
        public Tuple<bool, string> UpdateAnimal(Animal animal)
        {
            if (animalTypeBL.GetAnimalType(animal.AnimalTypeId).Id == 0)
                return Tuple.Create(false, "wrong data");
            else
                if (GetAnimal(animal.Id).Id != 0)
            {
                animalDL.UpdateAnimal(animal);
                return Tuple.Create(true, "success");
            }
            else
                return Tuple.Create(false, "not exists");

        }
        public Tuple<bool, string> DeleteAnimal(int id)
        {
            if (GetAnimal(id).Id != 0)
            {
                animalDL.DeleteAnimal(id);
                return Tuple.Create(true, "success");
            }
            else
                return Tuple.Create(false, "not exists");

        }

        public IEnumerable<Animal> GetAnimals()
        {
            return animalDL.GetAnimals();
        }
        public Animal GetAnimal(int id)
        {
            return animalDL.GetAnimal(id);
        }
        public Animal GetAnimalByName(string name)
        {
            return animalDL.GetAnimalByName(name);
        }
    }
}
