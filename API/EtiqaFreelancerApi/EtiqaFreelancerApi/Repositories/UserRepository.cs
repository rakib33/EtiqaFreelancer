using EtiqaFreelancerApi.DataContext;
using EtiqaFreelancerApi.Interfaces;
using EtiqaFreelancerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EtiqaFreelancerApi.Repositories
{
    public class UserRepository : IUser
    {
        FreelancerContext _context;
        public UserRepository(FreelancerContext freelancerContext) { 
            _context = freelancerContext;
        }
        public async Task<User> AddUser(User user)
        {
            try
            {
                await  _context.Users.AddAsync(user);
                 _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public void DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges(true);
                }
            }
            catch (Exception)
            {
                throw;
            }          
        }

        public async  Task<List<User>> GetUsers()
        {
           return await _context.Users.ToListAsync();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
