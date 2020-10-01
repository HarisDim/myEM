using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myEM.Models;
using myEM.Data;

namespace myEM.Api
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly CompanyContext _context;
        public EmployeesController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(Duration = 600)] //cache json for 1 minute
        public List<Employee> GetEmployees()
        {
            IQueryable<Employee> EmployeesIQuery = from e in _context.Employees
                                                   select e;
            return EmployeesIQuery.ToList() ;
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<bool> CreateNewEmployee(Employee newitem)
        {
            try
            {
                if (await TryUpdateModelAsync<Employee>(
                    newitem,
                    "employee",
                    e => e.FirstName, e => e.LastName, e => e.HiringDate, e => e.Details))
                {
                    _context.Employees.Add(newitem);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch(Exception ex) {
                Console.WriteLine(ex);    
                return false;
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<bool> UpdateEmployee(Employee updateitem)
        {
            try
            {
                var employeeToUpdate = await _context.Employees.FindAsync(updateitem.ID);

                if (employeeToUpdate == null)
                {
                    Console.WriteLine("Employee not found");
                    return false;
                }
                employeeToUpdate.Details = updateitem.Details;
                employeeToUpdate.FirstName = updateitem.FirstName;
                employeeToUpdate.LastName = updateitem.LastName;
                employeeToUpdate.HiringDate = updateitem.HiringDate;
                if (await TryUpdateModelAsync<Employee>(employeeToUpdate, "Employee",
                    e => e.FirstName, 
                    e => e.LastName, 
                    e => e.HiringDate, 
                    e => e.Details))
                {
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Delete(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("ID cannot be null");
                return false;
            }

           var Employee = await _context.Employees.FindAsync(id);

            if (Employee == null)
            {
                Console.WriteLine("Employee not found");
                return false;
            }

            try
            {
                _context.Employees.Remove(Employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
