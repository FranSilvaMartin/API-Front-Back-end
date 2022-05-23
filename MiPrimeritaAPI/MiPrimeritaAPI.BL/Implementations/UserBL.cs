using AutoMapper;
using MiPrimeritaAPI.BL.Contracts;
using MiPrimeritaAPI.CORE.DTO;
using MiPrimeritaAPI.CORE.Utils;
using MiPrimeritaAPI.DAL.Contracts;
using MiPrimeritaAPI.DAL.Tables;

namespace MiPrimeritaAPI.BL.Implementations
{
    public class UserBL : IUserBL
    {
        public IUserDAL userDAL { get; set; }
        public IMapper mapper { get; set; }

        public UserBL(IUserDAL alumnoDAL, IMapper mapper)
        {
            this.userDAL = alumnoDAL;
            this.mapper = mapper;
        }
        public bool Delete(string Username)
        {
            userDAL.Delete(Username);
            var alumno = userDAL.getUserByUsername(Username);
            var alumno2 = userDAL.getUserByEmail(Username);
            if (alumno == null || alumno2 == null)
            {
                return true;
            }
            return false;
        }

        public UserDTO? getUserByUsername(string username)
        {
            var user = userDAL.getUserByUsername(username);
            if (user == null)
                return null;
            return mapper.Map<UserDTO>(user);
        }

        

        public List<UserDTO> getUsers()
        {
            var alumnos = userDAL.GetAlumnos();
            var alumnoDTOs = mapper.Map<List<UserDTO>>(alumnos);
            return alumnoDTOs;  
        }

        public bool Insert(UserDTO alumnoDTO)
        {
            var alumno = userDAL.getUserByUsername(alumnoDTO.UserName);
            var alumno2 = userDAL.getUserByEmail(alumnoDTO.Email);
            if (alumno == null && alumno2 == null)
            {
               alumno = mapper.Map<User>(alumnoDTO);
               
               if (alumno.Password.Length >= 6 && alumno.Email.Contains("@"))
                {
                    alumno.Password = PasswordUtils.HashPassword(alumno.Password);

                    userDAL.register(alumno);
                    return true;
                } 
               return false;
            }
            return false;
        }

        public bool Update(string currentUser, UserDTO alumnoDTO)
        {
            var alumno = userDAL.getUserByUsername(currentUser);
            var alumno2 = mapper.Map<User>(alumnoDTO);

            if (alumno != null)
            {
                if (alumno.Password.Length >= 6)
                {
                    userDAL.Update(currentUser, alumno2);
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool login(LoginDTO loginDTO)
        {

            string hashPassword2 = "";

            if (loginDTO.Username.Contains("@"))
            {
                var user = userDAL.getUserByEmail(loginDTO.Username);

                if (user == null)
                    return false;
                if (PasswordUtils.VerifyHashedPassword(user.Password, loginDTO.Password))
                    return true;
            }
            else
            {
                var user = userDAL.getUserByUsername(loginDTO.Username);

                if (user == null)
                    return false;
                if (PasswordUtils.VerifyHashedPassword(user.Password, loginDTO.Password))
                    return true;
            }
            return false;
        }

        public UserDTO? getUserByEmail(string email)
        {
            var alumno = userDAL.getUserByEmail(email);
            if (alumno != null)
            {
                var alumnoDTO = mapper.Map<UserDTO>(alumno);
                return alumnoDTO;
            }
            else
            {
                return null;
            }
        }
    }
}