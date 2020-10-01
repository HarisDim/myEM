using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using myEM.Data;
using myEM.Models;

namespace myEM.Pages.Skills
{
    public class DetailsModel : PageModel
    {
        private readonly CompanyContext _context;

        public DetailsModel(CompanyContext context)
        {
            _context = context;
        }

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
    }
}
