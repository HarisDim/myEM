# myEM
An Employee Management app

For this project I used the following technologies:

- ASP.NET Core with Razor Pages to build the web app
- Entity Framework for data access
- Swagger for the API implementation
- MSSQL local dB for the database

The database has 2 tables: **Employee** and **Skill**

- **Employee** table contains the ID as the primary key ,the employee&#39;s First Name, Last Name, Hiring Date, Details and his Skill\_IDs which is a string that references the IDs of the skills he has
- **Skill** table contains the ID as the primary key, the skill&#39;s Name, it&#39;s details as well as the Date of its Creation

In this app the user can:

- View all the skills.
- Create, Update and Delete the skill as well as view the skill&#39;s Details
- View all the employees
- Create, Update and Delete an employee as well as view the Employee&#39;s Details
- Assign skills to an Employee
- Search Employees by their First Name/Last Name and order them by their Last Name/Hiring Date

For the API Documentation please access the following link: https://localhost:44349/swagger/index.html
