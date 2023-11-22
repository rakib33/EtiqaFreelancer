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
1. Create a Models folder and User.cs model class for freelancers.
2. Add FreelancerContext.cs DbContext class and configure database connection.
3. Add user db set in FreelancerContext.cs n OnModelCreating method.
4. In this project I am using Microsoft SQL Server RDBMS.
5. Database connection string is configure in FreelancerContext.cs
   ```
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-OC677T4;Initial Catalog=EtiqaFreelancerDB;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
   ```
6. In appsettings.json you can also mention the database connection string depends on your project purpose.
   But this is maybe harmful for project data security.
   ```
   "ConnectionStrings": {
    "DefaultConnection": "Data Source=DESKTOP-OC677T4;Initial Catalog=EtiqaFreelancerDB;Integrated Security=True"
    }
7. Create a basic BaseApiController.cs to write common task like logging.
8. Create UsersController.cs controller that has GET,POST,PUT,DELETE verbs method accordingly Get all user,Add user, update user and delete user.
9. Create an Interface named IUser and implement this in UserRepository.cs with below method.
   - GetUsers [Get all user list]
   - RegisterUser [Register or add a user]
   - UpdateUser [Update an existing user]
   - DeleteUser [Delete user]
10. Now register this interface and repository and our database context in Program.cs file for Dependency Injection (DI).
    ```
    builder.Services.AddScoped<IUser,UserRepository>();

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
And Main branch has local data storage.Hear we Use MS SQL Server.

Heroku Project: https://freelancer33-f16a7093777b.herokuapp.com/

