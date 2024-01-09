using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BusinessObjectLayer;

namespace TechnicalExercise.Controllers
{
    public class LoginController : ApiController
    {
        private LoginBL loginBL;
        public LoginController()
        {
            loginBL = new LoginBL();
        }
        [AllowAnonymous]
        public IHttpActionResult UserLogin(Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            var user = loginBL.UserLogin(login);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}