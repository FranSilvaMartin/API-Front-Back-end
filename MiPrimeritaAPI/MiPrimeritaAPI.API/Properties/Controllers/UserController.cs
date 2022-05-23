using Microsoft.AspNetCore.Mvc;
using MiPrimeritaAPI.BL.Contracts;
using MiPrimeritaAPI.BL.Implementations;
using MiPrimeritaAPI.CORE.DTO;

namespace MiPrimeritaAPI.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserBL userBL { get; set; }
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>
        /// Esto sirve para insertar un alumno.
        /// </summary>
        /// <param name="a">Alumno a insertar.</param>
        [HttpPost]
        [Route("Register")]
        public ActionResult register(UserDTO a) 
        {
            return Ok(userBL.Insert(a));
        }

        [HttpGet("All")]
        public ActionResult<List<UserDTO>> getAlumnos()
        {
            return Ok(userBL.getUsers());
        }

        [HttpGet]
        [Route("Get/username")]
        public ActionResult<UserDTO> getUserByUsername(string username)
        {
            return Ok(userBL.getUserByUsername(username));
        }

        [HttpGet]
        [Route("Get/email")]
        public ActionResult<UserDTO> getUserByEmail(string email)
        {
            return Ok(userBL.getUserByEmail(email));
        }

        [HttpPost]
        [Route("Update")]
        public ActionResult<UserDTO> update(string currentUser, UserDTO alumnoDTO)
        {
            return Ok(userBL.Update(currentUser, alumnoDTO));
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult delete(string username)
        {
            return Ok(userBL.Delete(username));            
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult login(LoginDTO loginDTO)
        {
            return Ok(userBL.login(loginDTO));
        }
    }
}
