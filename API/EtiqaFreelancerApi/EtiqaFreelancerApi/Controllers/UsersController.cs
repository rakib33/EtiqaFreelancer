using EtiqaFreelancerApi.Common;
using EtiqaFreelancerApi.DataContext;
using EtiqaFreelancerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("GetUserList")]
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
        
    }
}
