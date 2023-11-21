# Etiqa Freelancer

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

![image](https://github.com/rakib33/EtiqaFreelancer/assets/10026710/84781584-ee9d-4d7d-9156-e0b426bde2ff)

Go to the next button and in Project Name section give project name EtiqaFreelancer. Select Next for all comming page.Lets see the API project is created.

# Install Package
  - Install package Microsoft.EntityFrameworkCore version 6.0.14
  - Microsoft.EntityFrmaeworkCore Version-6.0.14
  - Microsoft.EntityFrmaeworkCore.SqlServer Version-6.0.14
  - Microsoft.EntityFrmaeworkCore.Tools Version-6.0.14
  - Microsoft.AspNetCore.Cors Version-2.2.0
    
# Project Structure
1. Create a Models folder and User.cs model class for freelancers.
2. Add FreelancerContext.cs DbContext class
