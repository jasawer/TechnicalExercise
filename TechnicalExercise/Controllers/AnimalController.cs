using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessObjectLayer;


namespace TechnicalExercise.Controllers
{
    [Authorize]
    public class AnimalController : ApiController
    {
        private AnimalBL animalBL;
        public AnimalController()
        {
            animalBL = new AnimalBL();
        }

        [AllowAnonymous]
        public IEnumerable<Animal> GetAnimals()
        {
            return animalBL.GetAnimals();
        }

        [AllowAnonymous]
        public IHttpActionResult GetAnimal(int id)
        {

            var animal = animalBL.GetAnimal(id);
            if (animal.Id == 0)
                return NotFound();
            return Ok(animal);

        }

        public IHttpActionResult PostNewAnimal(Animal animal)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            Tuple<bool, string> response = animalBL.CreateAnimal(animal);
            if (response.Item1)
                return Ok(response.Item2);
            else
                return BadRequest(response.Item2);
        }

        public IHttpActionResult PutAnimal(Animal animal)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            Tuple<bool, string> response = animalBL.UpdateAnimal(animal);
            if (response.Item1)
                return Ok(response.Item2);
            else
                return BadRequest(response.Item2);
        }

        public IHttpActionResult DeleteAnimal(int id)
        {
            var animal = animalBL.GetAnimal(id);
            if (animal == null)
                return NotFound();
            Tuple<bool, string> response = animalBL.DeleteAnimal(id);
            if (response.Item1)
                return Ok(response.Item2);
            else
                return BadRequest(response.Item2);
        }


    }
}
