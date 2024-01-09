using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjectLayer;
using DataAccessLayer;

namespace BusinessLayer
{
    public class AnimalTypeBL
    {
        private AnimalTypeDL animalTypeDL;
        public AnimalTypeBL()
        {
            animalTypeDL = new AnimalTypeDL();
        }
        public Tuple<bool, string> CreateAnimalType(AnimalType animalType)
        {
            if (GetAnimalTypeByName(animalType.Name).Id == 0)
            {
                animalTypeDL.CreateAnimalType(animalType);
                return Tuple.Create(true, "success");
            }
            else
                return Tuple.Create(false, "duplicated");

        }
        public Tuple<bool, string> UpdateAnimalType(AnimalType animalType)
        {
            if (GetAnimalType(animalType.Id).Id != 0)
            {
                animalTypeDL.UpdateAnimalType(animalType);
                return Tuple.Create(true, "success");
            }
            else
                return Tuple.Create(false, "not exists");

        }
        public Tuple<bool, string> DeleteAnimalType(int id)
        {
            if (GetAnimalType(id).Id != 0)
            {
                animalTypeDL.DeleteAnimalType(id);
                return Tuple.Create(true, "success");
            }
            else
                return Tuple.Create(false, "not exists");

        }

        public IEnumerable<AnimalType> GetAnimalTypes()
        {
            return animalTypeDL.GetAnimalTypes();
        }

        public AnimalType GetAnimalType(int id)
        {
            return animalTypeDL.GetAnimalType(id);
        }

        public AnimalType GetAnimalTypeByName(string name)
        {
            return animalTypeDL.GetAnimalTypeByName(name);
        }
    }
}
