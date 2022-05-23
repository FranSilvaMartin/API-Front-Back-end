
using MiPrimeritaAPI.DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeritaAPI.DAL.Contracts
{
    public interface IUserDAL
    {
        public void register(User a);
        public List<User> GetAlumnos();

        public User? getUserByUsername(string username);
        public User? getUserByEmail(string email);
        public bool Update(string currentUser, User alumnoDTO);
        public void Delete(string Nombre);

        public bool login(string nombre, string password);

    }
}