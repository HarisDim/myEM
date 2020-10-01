using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Internal;
using myEM.Data;
using myEM.Models;

namespace myEM.Pages.Skills
{
    public class CreateModel : PageModel
    {
        private readonly CompanyContext _context;
        public string ErrorMessageName { get; set; }

        public CreateModel(CompanyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Skill Skill { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            bool SkillNameExists = false;
            
            var newSkill = new Skill();

            if (await TryUpdateModelAsync<Skill>(
                newSkill, 
                "skill", 
                s => s.SkillName, s => s.Details))
            {
                if(_context.Skills.Any(s =>s.SkillName == newSkill.SkillName)) 
                {
                    SkillNameExists = true;
                    ErrorMessageName = "A Skill with the same name exists ";
                }

                if (!SkillNameExists)
                {
                    newSkill.DateOfCreation = DateTime.UtcNow;
                    _context.Skills.Add(newSkill);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}
