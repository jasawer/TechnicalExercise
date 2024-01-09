using BusinessLayer;
using BusinessObjectLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TechnicalExercise.Controllers
{
    [Authorize]
    public class AnimalTypeController : ApiController
    {
        private AnimalTypeBL animalTypeBL;
        public AnimalTypeController()
        {
            animalTypeBL = new AnimalTypeBL();
        }

        [AllowAnonymous]
        public IEnumerable<AnimalType> GetAnimalTypes()
        {
            return animalTypeBL.GetAnimalTypes();
        }

        [AllowAnonymous]
        public IHttpActionResult GetAnimalType(int id)
        {

            var AnimalType = animalTypeBL.GetAnimalType(id);
            if (AnimalType.Id == 0)
                return NotFound();

            return Ok(AnimalType);

        }

        public IHttpActionResult PostNewAnimalType(AnimalType AnimalType)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            Tuple<bool, string> response = animalTypeBL.CreateAnimalType(AnimalType);
            if (response.Item1)
                return Ok(response.Item2);
            else
                return BadRequest(response.Item2);
        }

        public IHttpActionResult PutAnimalType(AnimalType AnimalType)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            Tuple<bool, string> response = animalTypeBL.UpdateAnimalType(AnimalType);
            if (response.Item1)
                return Ok(response.Item2);
            else
                return BadRequest(response.Item2);
        }

        public IHttpActionResult DeleteAnimalType(int id)
        {
            var animalType = animalTypeBL.GetAnimalType(id);
            if (animalType.Id == 0)
                return NotFound();

            Tuple<bool, string> response = animalTypeBL.DeleteAnimalType(id);
            if (response.Item1)
                return Ok(response.Item2);
            else
                return BadRequest(response.Item2);
        }


    }
}