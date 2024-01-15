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
        public UsersController(IUser user, ILogger<BaseApiController> logger) : base(logger)
        {
            _user = user;
        }

        //[DisableCors]
        [HttpGet]
       // [ResponseCache(Duration =1)]
        public async Task<ActionResult<List<User>>> Get(string key)
        {
            try
            {
                var userList = await _user.GetUsers();
                if(userList == null || userList.Count == 0)
                {
                    return Ok(new { status = HttpStatusCode.NotFound });
                    //return Ok(HttpStatusCode.NotFound);
                }
                if(!string.IsNullOrEmpty(key))
                {
                    //key= key.ToLower();
                    userList = userList.Where(t => t.UserName.Contains(key) || t.PhoneNumber.Contains(key) || t.Email.Contains(key)).ToList();
                }

                return Ok(new { status = AppStatus.SuccessStatus, data = userList });
            }
            catch (Exception ex)
            {
                return Ok(new { status = AppStatus.ErrorStatus, data = ex });
            }       

        }

       

        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromForm] User user)
        {
            try
            {
                #region FileUpload
                //var file = Request.Form.Files[0]; //for single file
                //var formFiles = Request.Form.Files; //Request.Form.Files .get all file
                //var folderName = Path.Combine("wwwroot", "uploads");
                //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                //if (file.Length > 0)
                //{
                //    var fileName = Path.Combine(pathToSave, file.FileName);
                //    using (var stream = new FileStream(fileName, FileMode.Create))
                //    {
                //        file.CopyTo(stream);
                //    }

                //    return Ok(new { fileName });
                //}
                //else
                //{
                //    return BadRequest();
                //}
                //foreach (var formFile in formFiles)
                //{
                //    if (formFile.Length > 0)
                //    {
                //        using (var memoryStream = new MemoryStream())
                //        {
                //            await formFile.CopyToAsync(memoryStream);

                //            // Convert the IFormFile to a byte array
                //            var fileData = memoryStream.ToArray();

                //            // Save the file data to the database
                //            //var fileModel = new FileModel
                //            //{
                //            //    FileName = formFile.FileName,
                //            //    FileData = fileData
                //            //};

                //            user.FileName = formFile.FileName;
                //            user.FileData = fileData;                         
                //        }
                //    }
                //}
                #endregion

                User saveUser = await  _user.AddUser(user);
                return Ok(new UserResponseModel { Status = AppStatus.SuccessStatus, Data = saveUser });
            }
            catch (Exception ex)
            {
               // return StatusCode(500, $"Internal server error: {ex}");
               return Ok(new UserResponseModel { Status = AppStatus.ErrorStatus, Exception = ex});
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromForm] User user)
        {
            try
            {
                var updateUser =  _user.UpdateUser(user);
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
                return Ok(new UserResponseModel { Status = AppStatus.SuccessStatus });
            }
            catch (Exception ex)
            {
                return Ok(new UserResponseModel { Status = AppStatus.ErrorStatus, Exception = ex });
            }
        }
    }
}
