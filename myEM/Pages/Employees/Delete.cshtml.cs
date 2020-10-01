using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using myEM.Models;
using myEM.Data;

namespace myEM.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly CompanyContext _context;

        public DeleteModel(CompanyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Employee == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employees.FindAsync(id);

            if (Employee == null)
            {
                return NotFound();
            }

            try
            {
                _context.Employees.Remove(Employee);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException)
            {            
                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}
