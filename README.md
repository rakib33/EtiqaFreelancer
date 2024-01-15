# Simple user crud app using ASP.NET Core Web API(.Net 6) and Vue.js repository pattern and xUnit test

This is ASP.NET Core Web API sample project of a list of data of Freelancer users.
This is a complete Web Api project to register, delete ,update and get all freelancer.
Project link: https://freelancer33-f16a7093777b.herokuapp.com/

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
 
 1. Create a xUnit Test Project named EtiqaFreelancerApi.Test and add class UsersControllerTest.cs to test all our UsersController method.
 2. In UsersControllerTest constructor arrange all dependencies using Moq object.
     ```
        private readonly UsersController _usersController;
        //Moq
        private readonly Mock<IUser> _user;
        private readonly Mock<ILogger<BaseApiController>> _logger;
        public UsersControllerTest()
        {
            _user = new Mock<IUser>();
            _logger= new Mock<ILogger<BaseApiController>>();
            _usersController = new UsersController(_user.Object,_logger.Object);
        }
     ```
 3. Write GetUserList_ReturnActionResult test method to test GET api.
     ```
       [Fact]
        public void GetUserList_ReturnActionResult()
        {
            //Arrange
            var ExpectedResut = new List<User> { };
            //act
            var result = _usersController.Get();
            //assert
            Assert.NotNull(result);    
            Assert.IsAssignableFrom<Task<ActionResult<List<User>>>>(result);            
        }
     ```
 
 4. Write AddUser_ReturnActionResult test method to test RegisterUser api.
    ```
        [Fact]
        public async void AddUser_ReturnActionResult()
        {
            //Arrange
            var userModel = Mock.Of<User>(x=>x.UserName =="Rakibul Islam" && x.Email =="rakib@gmail.com" && x.Hobby=="Travelling" && x.SkillSets == "ASP.NET 
            Core");
            //Act
            var result = await _usersController.RegisterUser(userModel);   
            //Assert
            Assert.NotNull(result);
            //assert            
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsType<UserResponseModel>(okObjectResult.Value);
            Assert.True(resultValue.Status == AppStatus.SuccessStatus);
        }
    ```
 5. Write UpdateUser_ReturnActionResult test methodto test UpdateUser api.
    ```
        [Fact]
        public async void UpdateUser_ReturnActionResult()
        {
            //Arrange
            var userModel = Mock.Of<User>(x => x.Id ==1 && x.UserName == "Rakibul Islam" && x.Email == "rakib@gmail.com" && x.Hobby == "Travelling" && 
            x.SkillSets == "ASP.NET Core");
            //Act
            var result = await _usersController.UpdateUser(userModel);
            //Assert
            Assert.NotNull(result);
            //assert            
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsType<UserResponseModel>(okObjectResult.Value);
            Assert.True(resultValue.Status == AppStatus.SuccessStatus);
        }
    ```
 6. Write DeleteUser_ReturnActionResult test method to test DeleteUser api.
    ```
        [Fact]
        public async void DeleteUser_ReturnActionResult()
        {
            //Arrange
            User userModel = Mock.Of<User>(x => x.Id == 1 && x.UserName == "Rakibul Islam" && x.Email == "rakib@gmail.com" && x.Hobby == "Travelling" &&     
            x.SkillSets == "ASP.NET Core");
            //Act     
            var result = _usersController.DeleteUser((int)userModel.Id);
            //Assert
            Assert.NotNull(result);
            //assert            
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsType<UserResponseModel>(okObjectResult.Value);
            Assert.True(resultValue.Status == AppStatus.SuccessStatus);
        }
    ```
 7. Now run the all test method to make sure all are passed successsfully.

## Create vue.js client project

We are using vue.js for this application. you need to install node js and then vue cli.
Download node js and install from here https://nodejs.org/en/download/current
open root directory and run cmd .check the npm version by using this command.

D:\PROJECT Asp.NetCore\Freelancer>npm -v
9.6.0

Now I will install vue cli globally on Freelancer directory by running below command.
```
npm install -g @vue/cli
```
Check the vuejs version .
vue -V
@vue/cli 5.0.8

now I want to create my client project named vue-client, run the below command

```
vue create vue-client
```
While run  the above command it will ask which version you want to install. I am selected Vue 3 version.

Vue CLI v5.0.8
? Please pick a preset:
> Default ([Vue 3] babel, eslint)
  Default ([Vue 2] babel, eslint)
  Manually select features

now run or build the project.
```
npm run serve
```
## Structure
This simple Vue.js vue client project structure is looks like,

![image](https://github.com/rakib33/Freelancer/assets/10026710/5cc1ccdb-4cf3-4c98-b151-e0bfbf36eda3)


# Components

We are using very simple user crud application so we need to do create user component.
 - [UserList.vue] -> This is responsible to display user list on bootstrap table.
 - [AddUser.vue] -> This is responsible to add a new user.
 - [UserDetails.vue] -> This is responsible to display indivvidual user data beside list.

# Router

For navigating url like Add user , User List and so on we need to create a router.js 

```
import { createWebHistory, createRouter } from "vue-router";

const routes  =  [
  {
    path: "/",
    alias: "/users",
    name: "users",
    component: () => import("./components/UsersList")
  }, 
  {
    path: "/add",
    name: "add",
    component: () => import("./components/AddUser")
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
```
# HTTP Client

For http api communication install axios. 

```
npm install --save axios vue-axios
```
Create a js file http-common.js , we are configure application api here.

```
import axios from "axios";

export default axios.create({
  baseURL: "https://localhost:7222/api",
  withCredentials : true, 
   headers: {
     "Content-type": "application/json",
     'Access-Control-Allow-Origin': '*' // Specify the allowed origins (replace * with your actual domain in production)
   }
});
```

# App.vue

App.vue has master templete header footer and routing url or any thing you can do here.

```
<template>
  <div id="app">
    <nav class="navbar navbar-expand navbar-dark bg-dark">
      <router-link to="/" class="navbar-brand">Users</router-link>
      <div class="navbar-nav mr-auto">
        <li class="nav-item">
          <router-link to="/users" class="nav-link">Users</router-link>
        </li>
        <li class="nav-item">
          <router-link to="/add" class="nav-link">Add</router-link>
        </li>
      </div>
    </nav>

    <div class="container mt-3">
      <router-view />
    </div>
  </div>
</template>

<script>
export default {
  name: "app"
};
</script>
```

# service

service has all api calling method and business calculation. This method is called from component according their need.

```
import http from '../http-common';


class UserDataService {
    getAll() {   
      return http.get("/Users");
    }
  
    get(id) {
      return http.get(`/Users/${id}`);
    }
  
    create(data) {    
      return http.post("/Users",data);
    }
  
    update(id, data) {
      return http.put(`/Users/${id}`, data);
    }
  
    delete(id) {
      return http.delete(`/Users?id=${id}`);
    }
  
    deleteAll() {
      return http.delete(`/Users`);
    }
  
    //key hear user name || email || phone no
    findByKey(key) {
      return http.get(`/Users?key=${key}`);
    }
  }
  
  export default new UserDataService();
```

# FormComponent
We create our own reusable custom form component like input text field, email input field or telephone field etc.

![image](https://github.com/rakib33/Freelancer/assets/10026710/85cc7661-7179-4f95-892e-6da3b753bd14)

# Email Input Field 
Lets look at our custom email input field . This field contains basic email template , properties and methods for validation.

```
<template>
         <div class="form-group mb-3 mt-3">
            <label :for="id">{{ label }}</label>
            <input
            class="form-control"
            v-model="email"
            type="email"
            :class="{ 'is-invalid': showError}"
            :placeholder="placeholder"
            :id="id"
            :name="id"
            :required="required"
            @input="validateEmail"
            />
            <div v-if="showError" class="error-message">{{ error }}</div>
         </div>   
</template>
<script>
export default{
    props: {
    id: {
      type: String,
      required: true,
    },
    label: {
      type: String,
      required: true,
    },
    required:{
      type: Boolean, default: false
    },
    validator: {
      type: Function,
      default: () => () => false,
    },
    placeholder:{
      type: String,
      required:false,
      default:'',
    }, 
    isEmailValid:{
      type:Boolean,
      default:false,
    }
  },
 data(){
    return{
        email:'',
        error:'',
        showError:false,
    };
 },
 methods:{
    validateEmail(){
      console.log('émail valiadetEmail is called.');
         // Basic email validation using a regular expression
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if(emailRegex.test(this.email)){
      this.error ='';
      this.showError = false;
    
      }else{
        this.showError = true;
        this.error = 'Invalid email address';
      }
    
      this.$emit('update:modelValue', this.email);
    },
 },

};
</script>

<style scoped>
.is-invalid {
  border: 1px solid red;
}
.error-message {
  color: red;
  font-size: 0.8em;
  margin-top: 0.2em;
}
</style>
```

# Build 
To create a production build open visual studio code terminal. short key [Ctrl+Shift+~] and run this command. This will create a dist folder.

```
npm run build
```

Dist folder conatins all our packages.

![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/2ee60836-c9ba-4d67-adc0-643b65847e03)

Now copy all file and past visual studio wwwroot folder

![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/971b7ffa-894e-455f-a55d-8c5b43997a50)


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

   ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/09a2f499-74e7-4b25-a2aa-93a11efe1a14)


2. Add user button will open user modal to add new user and update the user list.

   ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/3b122d3e-2d80-466d-b7a0-ff3228faa1c6)


3. Click delete button to delete a user and after success list will be updated.

   ![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/cc8794e3-d51a-45b6-9e03-236acd303aa0)

   


    

