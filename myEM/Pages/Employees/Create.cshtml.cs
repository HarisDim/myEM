using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myEM.Models;
using myEM.Data;

namespace myEM.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly CompanyContext _context;

        public CreateModel(CompanyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnPostAsync() 
        {
            var newEmployee = new Employee();
            if (await TryUpdateModelAsync<Employee>(
                newEmployee,
                "employee",   // Prefix for form value.
                e => e.FirstName, e => e.LastName, e => e.HiringDate, e =>e.Details))
            {
                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
