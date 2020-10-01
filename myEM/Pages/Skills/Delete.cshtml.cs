using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using myEM.Data;
using myEM.Models;

namespace myEM.Pages.Skills
{
    public class DeleteModel : PageModel
    {
        private readonly CompanyContext _context;

        public DeleteModel(CompanyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Skill Skill { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Skill = await _context.Skills.FirstOrDefaultAsync(m => m.SkillID == id);

            if (Skill == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Skill = await _context.Skills.FindAsync(id);

            if (Skill != null)
            {
                _context.Skills.Remove(Skill);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
