using EtiqaFreelancerApi.Common;
using EtiqaFreelancerApi.DataContext;
using EtiqaFreelancerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EtiqaFreelancerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        private readonly FreelancerContext _context;
        public UsersController(FreelancerContext context, ILogger<BaseApiController> logger) : base(logger)
        {
            _context = context;
        }
               

        [HttpGet]
        [ResponseCache(Duration =60)]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var userList = await _context.Users.ToListAsync();
                return Ok(new { status = AppStatus.SuccessStatus, data = userList });
            }
            catch (Exception ex)
            {
                return Ok(new { status = AppStatus.ErrorStatus, data = ex });
            }       

        }

        [HttpPost]
        public JsonResult RegisterUser([FromForm] User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return new JsonResult("Added Successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult("Added Failed");
            }
        }

        [HttpPut]
        public JsonResult UpdateUser([FromForm] User user)
        {
            try
            {
                User _user = _context.Users.Find(user.Id);
                if (_user == null)
                    return new JsonResult("No Data Found");

                    _user.UserName = user.UserName;
                    _user.PhoneNumber = user.PhoneNumber;
                    _user.Email = user.Email;
                    _user.Hobby = user.Hobby;
                    _user.SkillSets = user.SkillSets;

                    _context.Entry(_user).State = EntityState.Modified;
                    _context.SaveChanges();

                return new JsonResult("Deleted Successfully");
            }
            catch (Exception)
            {
                return new JsonResult("Deleted Failed");
            }

        }

        [HttpDelete]
        public JsonResult DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                    return new JsonResult("No Data Found");
                _context.Users.Remove(user);
                _context.SaveChanges(true);

                return new JsonResult("Deleted Successfully");
            }
            catch (Exception)
            {
                return new JsonResult("Deleted Failed");
            }
        }
    }
}
