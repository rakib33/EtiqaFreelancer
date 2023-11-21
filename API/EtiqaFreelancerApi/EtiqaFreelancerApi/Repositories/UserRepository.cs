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
            try
            {
                User _user = _context.Users.Find(user.Id);
                _user.UserName = user.UserName;
                _user.PhoneNumber = user.PhoneNumber;
                _user.Email = user.Email;
                _user.Hobby = user.Hobby;
                _user.SkillSets = user.SkillSets;

                _context.Entry(_user).State = EntityState.Modified;
                _context.SaveChanges();
                return _user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
