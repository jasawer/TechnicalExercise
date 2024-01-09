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
    public class UserController : ApiController
    {
        private UserBL userBL;
        public UserController()
        {
            userBL = new UserBL();
        }
        [AllowAnonymous]
        public IHttpActionResult PostNewUser(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            Tuple<bool, string> response = userBL.CreateUser(user);
            if (response.Item1)
                return Ok(response.Item2);
            else
                return BadRequest(response.Item2);
        }

    }
}