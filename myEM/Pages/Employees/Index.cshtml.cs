using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using myEM.Data;
using myEM.Models;

namespace myEM.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly CompanyContext _context;

        public IndexModel(CompanyContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public IList<Employee> Employees { get;set; }

        public async Task OnGetAsync(string sortOrder , string searchString)
        {
            NameSort = String.IsNullOrEmpty(sortOrder)? "name_desc" : "";
            DateSort = sortOrder == "date" ? "date_desc" : "date";

            CurrentFilter = searchString;

            IQueryable<Employee> EmployeesIQuery = from e in _context.Employees
                                             select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                EmployeesIQuery = EmployeesIQuery.Where(e => e.LastName.Contains(searchString)
                                       || e.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    EmployeesIQuery = EmployeesIQuery.OrderByDescending(e => e.LastName);
                    break;
                case "date":
                    EmployeesIQuery = EmployeesIQuery.OrderBy(e => e.HiringDate);
                    break;
                case "date_desc":
                    EmployeesIQuery = EmployeesIQuery.OrderByDescending(e => e.HiringDate);
                    break;
                default:
                    EmployeesIQuery = EmployeesIQuery.OrderBy(e => e.LastName);
                    break;
            }

            Employees = await EmployeesIQuery.AsNoTracking().ToListAsync();
        }
    }
}
