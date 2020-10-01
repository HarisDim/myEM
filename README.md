# myEM
An Employee Management app

For this project I used the following technologies:
    •	ASP.NET Core with Razor Pages to build the web app
    •	Entity Framework for data access
    •	Swagger for the API implementation
    •	MSSQL local dB for the database  
The database has 2 tables: Employee and Skill
    •	Employee table contains:
      o	ID as the primary key 
      o	First Name
      o	Last Name
      o	Hiring Date
      o	Details
      o	Skill_IDs (a string that references the IDs of the employee’s skills)
    •	Skill table contains:
      o	ID as the primary key
      o	Skill Name
      o	Details 
      o	Date of its Creation
In this app the user can:
    •	View all the skills
    •	Create, Update and Delete the skill as well as view the skill’s Details
    •	View all the employees
    •	Create, Update and Delete an employee as well as view the Employee’s Details
    •	Assign skills to an Employee
    •	Search Employees by their First Name/Last Name and sort them by their Last Name/Hiring Date 
For the API Documentation please access the following link: 
https://localhost:44349/swagger/index.html
