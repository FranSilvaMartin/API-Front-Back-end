using MiPrimeritaAPI.CORE.DTO;

namespace MiPrimeritaAPI.BL.Contracts
{
    public interface IUserBL
    {
        public bool Insert(UserDTO a);
        public List<UserDTO> getUsers();

        public UserDTO? getUserByEmail(string email);

        public UserDTO? getUserByUsername(string username);
        public bool Update(string currentUser, UserDTO alumnoDTO);
        public bool Delete(string Username);

        public bool login(LoginDTO loginDTO);
    }
}
