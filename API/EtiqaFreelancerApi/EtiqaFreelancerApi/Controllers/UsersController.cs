using EtiqaFreelancerApi.Cache;
using EtiqaFreelancerApi.Common;
using EtiqaFreelancerApi.DataContext;
using EtiqaFreelancerApi.Interfaces;
using EtiqaFreelancerApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.IO;
using System.Net;

namespace EtiqaFreelancerApi.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        private readonly IUser _user;
        private readonly ICacheService _cacheService;
        public UsersController(IUser user, ICacheService cacheService, ILogger<BaseApiController> logger) : base(logger)
        {
            _user = user;
            _cacheService = cacheService;
        }

        [DisableCors]
        [HttpGet]
        [ResponseCache(Duration =1)]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                //Check Data from cache.user is cache key
                var cacheData = _cacheService.GetData<List<User>>("user");
                if (cacheData != null)
                {
                    return Ok(new { status = AppStatus.SuccessStatus, data = cacheData });
                }
                else
                {
                    var userList = await _user.GetUsers();
                    if (userList == null || userList.Count == 0)
                    {
                        return Ok(HttpStatusCode.NotFound);
                    }

                    //cache expired time
                    var expirationTime = DateTimeOffset.Now.AddMinutes(2.0);
                    //set userList to cache
                    _cacheService.SetData<List<User>>("user", userList, expirationTime);
                    //return response
                    return Ok(new { status = AppStatus.SuccessStatus, data = userList });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = AppStatus.ErrorStatus, data = ex });
            }       

        }


        [HttpGet]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                User filteredData;
                var cacheData = _cacheService.GetData<List<User>>("user");
                if (cacheData != null)
                {
                    filteredData = cacheData.Where(x => x.Id == id).FirstOrDefault();
                    return filteredData;
                }

                var userList = await _user.GetUsers();
                filteredData = userList.Where(x => x.Id == id).FirstOrDefault();                
                return Ok(new { status = AppStatus.SuccessStatus, data = filteredData });
            }
            catch(Exception ex)
            {
                return Ok(new { ex.Message });
            }
        }


        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromForm] User user)
        {
            try
            {
                var formFiles = Request.Form.Files; //get all file             

                foreach (var formFile in formFiles)
                {
                    if (formFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(memoryStream);
                            // Convert the IFormFile to a byte array
                            var fileData = memoryStream.ToArray();
                            user.FileName = formFile.FileName;
                            user.FileData = fileData;
                        }
                    }
                }
                User saveUser = await  _user.AddUser(user);          
                RemoveUserCache();
                return Ok(new UserResponseModel { Status = AppStatus.SuccessStatus, Data = saveUser });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
              //  return Ok(new UserResponseModel { Status = AppStatus.ErrorStatus, Exception = ex});
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromForm] User user)
        {
            try
            {
                var updateUser =  _user.UpdateUser(user);
                RemoveUserCache();
                return Ok(new UserResponseModel { Status = AppStatus.SuccessStatus, Data = updateUser });
            }
            catch (Exception ex)
            {
                return Ok(new UserResponseModel { Status = AppStatus.ErrorStatus, Exception = ex });
            }

        }

        [HttpDelete]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                _user.DeleteUser(id);
                RemoveUserCache();
                return Ok(new UserResponseModel { Status = AppStatus.SuccessStatus });
            }
            catch (Exception ex)
            {
                return Ok(new UserResponseModel { Status = AppStatus.ErrorStatus, Exception = ex });
            }
        }

        public void SaveToLocalDirectory()
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("wwwroot", "uploads");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = Path.Combine(pathToSave, file.FileName);
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
        }

        private void RemoveUserCache()
        {
            //remove previous cache
            _cacheService.RemoveData("user");
        }
    }
}
