using MiPrimeritaAPI.DAL.Contracts;
using MiPrimeritaAPI.DAL.Tables;
using MiPrimeritaAPI.DAL.Utils;

namespace MiPrimeritaAPI.DAL.Implementations
{
    public class UserDAL : IUserDAL
    {
        public IESContext Context { get; set; }

        PasswordUtils passwordUtils = new PasswordUtils();

        public UserDAL(IESContext Context)
        {
            this.Context=Context;
        }
        public void Delete(string Nombre)
        {
            User? alumno = null;
            if (Nombre.Contains("@"))
            {
                alumno = getUserByEmail(Nombre);
            }
            else
            {
                alumno = getUserByUsername(Nombre);
            }

            if (alumno != null)
                Context.Users.Remove(alumno);
            Context.SaveChanges();
        }

        public List<User> GetAlumnos()
        {
            return Context.Users.ToList();
        }

        public void register(User a)
        {
            Context.Users.Add(a);
            Context.SaveChanges();
        }

        public bool Update(string currentUser, User alumnoDTO)
        {
            var alumnoBD = getUserByUsername(currentUser);
            if (alumnoBD != null)
            {
                alumnoBD.Username = alumnoDTO.Username;
                alumnoBD.Email = alumnoDTO.Email;
                alumnoBD.Password = PasswordUtils.HashPassword(alumnoDTO.Password);
                alumnoBD.DateBirth = alumnoDTO.DateBirth;
                Context.Users.Update(alumnoBD);
                Context.SaveChanges();
                return true;
            }
            return false;
        }
        
        public bool login(string nombre, string password)
        {

            User? user = null;
            if (nombre.Contains("@"))
            {
                user = getUserByEmail(nombre);
            }
            else
            {
                user = getUserByUsername(nombre);
            }


            if (user != null)
            {
                if (user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public User? getUserByUsername(string username)
        {
            return Context.Users.FirstOrDefault(a => a.Username == username);
        }

        public User? getUserByEmail(string email)
        {
            return Context.Users.FirstOrDefault(a => a.Email == email);
        }
    }
}
