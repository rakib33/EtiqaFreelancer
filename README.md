# Freelancer

This is ASP.NET Core Web API sample project of a list of data of Freelancer users.
This is a complete Web Api project to register, delete ,update and get all freelancer.


## Project Summary

A company create a developer network to build a list of freelancer.

1. Need to develop API to register/delete/update/get for an user using verbs @GET,@POST,@PUT,@DELETE .

2. The freelancer user model should have below attributes:
    - UserName
    - Mail
    - Phone Number
    - Skillsets
    - Hobby

3. Communicate any well-know RDBMS database to  demonastrate data storage.

4. Need to create a sample Interface and Repository pattern for below operations:
    - GET list of register user name.
    - Register a new user.
    - Update a register user details
    - Delete a register user.
      
## Technology Used
- ASP.NET Core Web API
- Framework .Net 6
- Database MS SQL Server
- Vue.Js for UI

## Get started

To get started developing a Web API in Visual Studio the first step is to create a new project. In Visual Studio 2022 you can create a new project using the New Project dialog. In this post we will create an ASP.NET Core Web API for Etiqa Freelancer project. To follow along this tutorial, create a project named EtiqaFreelancer with the following options selected in the Additional Information page.

![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/2423f9d2-c7f7-46d5-8275-763df29dab19)

Go to the next button and in Project Name section give project name EtiqaFreelancerApi. 

![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/9e9897ec-eee2-4125-b1f5-0702200fbcfc)

In the next page choose a .Net Framework . In this project I am using .NET 6.0 . You can also select Aunthentication type. Here is two type Authentication.
 - Microsoft Identity Platform
 - Windows

![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/09a6e276-20a4-4b94-b235-dfc66bb630e8)

After press the Create button Lets see the API project is created.The project infrastructure is looks like.

![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/9ea5cd3d-32a9-4d66-82e5-514a3cde4a8f)

## Install Package
  - Install package Microsoft.EntityFrameworkCore version 6.0.14
  - Microsoft.EntityFrmaeworkCore Version-6.0.14
  - Microsoft.EntityFrmaeworkCore.SqlServer Version-6.0.14
  - Microsoft.EntityFrmaeworkCore.Tools Version-6.0.14
  - Microsoft.AspNetCore.Cors Version-2.2.0
    
## Project Structure
1. Create a Models folder and User.cs class for freelancers user.
   ```
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(13)]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? SkillSets   { get; set; }

        [Required]
        public string? Hobby { get; set; }

    }
   ```

2. Add FreelancerContext.cs class on DbContext folder and configure database connection on.
    ```
    using EtiqaFreelancerApi.Models;
    using Microsoft.EntityFrameworkCore;
    
    namespace EtiqaFreelancerApi.DataContext
    {
        public class FreelancerContext : DbContext
        {
            public FreelancerContext(DbContextOptions<FreelancerContext> options) : base(options)
            {
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=<YourDataSource>;Initial Catalog=<DatabaseName>;User ID=<UserName>;Password=****;Integrated Security=False");
                base.OnConfiguring(optionsBuilder);
            }
            public DbSet<User> Users { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<User>(b => {
                    b.HasKey(l => l.Id);
                    b.ToTable("User");
                });
            }
        }
    }

    ```

3. Add user db set in FreelancerContext.cs  OnModelCreating method.
4. In this project I am using Microsoft SQL Server database
5. Database connection string is configure in FreelancerContext.cs
   ```
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=<YourDataSource>;Initial Catalog=<DatabaseName>;User ID=<UserName>;Password=****;Integrated Security=False");
            base.OnConfiguring(optionsBuilder);
        }
   ```
6. In appsettings.json you can also mention the database connection string depends on your project purpose.
   But this is maybe harmful for project data security.
   ```
   "ConnectionStrings": {
    "DefaultConnection": "Data Source=<YourDataSource>;Initial Catalog=<DatabaseName>;User ID=<UserName>;Password=****;Integrated Security=False"
    }
7. Create a basic BaseApiController.cs to write common task like logging.
   ```
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private readonly ILogger<BaseApiController> _logger;

        public BaseApiController(ILogger<BaseApiController> logger)
        {
            _logger = logger;
        }

        // Common method for logging
        protected void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        // Common method for logging errors
        protected void LogError(string message)
        {
            _logger.LogError(message);
        }

        // Common method for handling bad requests with custom error message
        protected ActionResult BadRequest(string errorMessage)
        {
            LogError(errorMessage);
            return base.BadRequest(new { error = errorMessage });
        }
    }
   ```


8. Create an Interface named IUser in Interfaces folder and implement this in UserRepository.cs class in Repositories folder with below method.
   - GetUsers [Get all user list]
   - RegisterUser [Register or add a user]
   - UpdateUser [Update an existing user]
   - DeleteUser [Delete user]

   IUser.cs
    ```
    public interface IUser
    {
        public Task<List<User>> GetUsers();
        public Task<User> AddUser(User user);
        public User UpdateUser(User user);
        public void DeleteUser(int id);
    }
    ```
  UserRepository.cs
  ```
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
                _context.SaveChangesAsync();
                return _user;
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
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
          
        }
    }
 ```

10. Now register this interface and repository and our database context in Program.cs file for Dependency Injection (DI). Also add cors and caching.
    ```
    //Configure database context
    builder.Services.AddDbContext<FreelancerContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(5),
             errorNumbersToAdd: null);
        }));
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin().WithOrigins("https://freelancer33-f16a7093777b.herokuapp.com")
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials();
            });
    });
    
    builder.Services.AddScoped<IUser,UserRepository>();
    
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddResponseCaching();
    ```
11. Create UsersController.cs controller that has GET,POST,PUT,DELETE verbs method accordingly Get all user,Add user, update user and delete user.
    ```
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

        [DisableCors]
        [HttpGet]
        [ResponseCache(Duration =1)]
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
        public async Task<ActionResult> RegisterUser([FromForm] User user)
        {
            try
            {
               User saveUser = await  _user.AddUser(user);
                return Ok(new UserResponseModel { Status = AppStatus.SuccessStatus, Data = saveUser });
            }
            catch (Exception ex)
            {
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
    ```
## Database Migration

 MS SQL database is used for this project.Database code first migration follow the command after configuring database connection string.
 
  1. Open nuget package manager console.
 
  2. For initial migration run command
     ```
     Add-Migration InitialCreate
     ```
3. Migration folder is created on project directory. But still database is not created on server.Need to update database, run below command.

    ```
    Update-Database
    ```
4. After any change on database model you need to run Add Migration command with appropriate migration nameand must update-database each time.
   
# Unit Test Project

One xUnit test project is created to test our web api controler with moq data. For this you need to install unit test tools from nuget.

## Install test packages

   - Moq Version-4.20.69
   - xunit Version-2.4.2
   - unit.runner.visualstudio Version-2.4.5
     
## Users Controller Test
 
 1. Create a UsersControllerTest.cs to test all our UsersController method.
 2. In UsersControllerTest constructor all arrange task is done by passing ILogger and IUser Moq object.
 3. Write GetUserList_ReturnActionResult test method to test GET api.
 4. Write AddUser_ReturnActionResult test method to test RegisterUser api.
 5. Write UpdateUser_ReturnActionResult test methodto test UpdateUser api.
 6. Write DeleteUser_ReturnActionResult test method to test DeleteUser api.
 7. Now run the all test method to make sure all are passed successsfully.

## Deployment

This project has several branches including Main and Deploy. Deploy has contains Heroku configuration to deploy our project on Heroku.
And Main branch has local data storage.Hear we Use MS SQL Server. Heroku Project: https://freelancer33-f16a7093777b.herokuapp.com/

1. Create an account on Heroku and Login.
2. Create an app using Create New App

   ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/111205ed-9607-4366-872d-ee9fb5c84712)

3. Give a project name and Click Create App button.

   ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/4dcda2da-4a34-41be-8079-de3dda796b53)

5. Go to Resources and select a Database. I am using MS SQL Database for this project.

   ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/19716dc9-cc20-4784-b88d-d97c4fed99ff)

6. Click the selected Database and it will open in new browser . Select View Config.

   ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/68d1dbf3-6d57-49a8-a078-6200b5f5cc7f)
 
 7. click View config to get database connection string .

    ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/28491d00-a9b7-4beb-b27d-2b64c1899bb1)
 
 8. Now configure this Database connection string to our project . Opne appsettings and add database connection string . you can also add this connection on code side.

    ```
      {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "ConnectionStrings": {
        "DefaultConnection": "Data Source=<Host Name>;Initial Catalog=<Database Name>;User ID=<User ID>;Password=*********;Integrated Security=False"
    
      }
     }
    ```
9. After configuring database connection run ASP.NET Web Api project and check everything is working well.
10. Now select the deploy tab on Heroku and select your github repository

    ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/eaf3515d-0d2a-4f8b-b926-1c110b68209a)

    ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/23197275-fcc6-490c-996b-c03919f236de)

11. Select a branch name for deployment with automatic deploy so that each time when any push is done it will automatically deploy on this site.

    ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/ac4852c5-6945-439d-939c-069fb4cdbea9)

12. Now click the Setting tab.Though we are using ASP.NET Core Web API we need a build pack to build our project on heroku. There are many third party build pack.
    I am choose this build pack for my application https://github.com/jincod/dotnetcore-buildpack

    ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/0ad2cd94-0079-4c48-914b-8d1a3e5e96c3)

13. Now everything is ready to deploy our project. Go to deploy tab and select deploy barnch from dropdown if not selected yet and click deploy barnch button.

   ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/e3112944-3455-4624-9ebf-8260df999b48)

14. Select the Activity tab where all depoloyment history is recorded. When new deployment is inprogress you can check here.

    ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/668dc744-2414-4ea2-bcd3-adc6e7191dd3)

15. Click the Open App button and see your project is on live.

    ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/e549a7bb-7a2f-4c15-adbc-cd475ceeb013)

## Project UI

1. Index Page to display all user list.

    ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/7e782f69-e92d-42b3-b2a8-f0e5dc26cd03)

2. Add user button will open user modal to add new user and update the user list.

   ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/4272b163-b18c-4b18-8ba1-70b2173db355)

3. Click delete button to delete a user and after success list will be updated.

   ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/2f888b24-a36f-4f07-86cf-89225ae555c0)
   


    

