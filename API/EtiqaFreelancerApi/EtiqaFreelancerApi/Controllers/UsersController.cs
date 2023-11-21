using EtiqaFreelancerApi.Common;
using EtiqaFreelancerApi.DataContext;
using EtiqaFreelancerApi.Interfaces;
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
        private readonly IUser _user;
        public UsersController(IUser user, ILogger<BaseApiController> logger) : base(logger)
        {
            _user = user;
        }

        [HttpGet]
        [ResponseCache(Duration =60)]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var userList = await _user.GetUsers();
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
               var saveUser =  _user.AddUser(user);
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
                var updateUser = _user.UpdateUser(user);
                return new JsonResult("Update Successfully");
            }
            catch (Exception)
            {
                return new JsonResult("Update Failed");
            }

        }

        [HttpDelete]
        public JsonResult DeleteUser(int id)
        {
            try
            {
                _user.DeleteUser(id);
                return new JsonResult("Deleted Successfully");
            }
            catch (Exception)
            {
                return new JsonResult("Deleted Failed");
            }
        }
    }
}
