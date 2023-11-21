using EtiqaFreelancerApi.Models;

namespace EtiqaFreelancerApi.Interfaces
{
    public interface IUser
    {
        public Task<List<User>> GetUsers();
        public Task<User> AddUser(User user);
        public User UpdateUser(User user);
        public void DeleteUser(int id);

    }
}
