using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myEM.Data;
using myEM.Models;

namespace myEM.Pages.Skills
{
    public class EditModel : PageModel
    {
        private readonly CompanyContext _context;

        public EditModel(CompanyContext context)
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

            Skill = await _context.Skills.FindAsync(id);

            if (Skill == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var skillToUpdate = await _context.Skills.FindAsync(id);
            if (skillToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Skill>(skillToUpdate, "Skill", s => s.SkillName, s => s.Details))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
