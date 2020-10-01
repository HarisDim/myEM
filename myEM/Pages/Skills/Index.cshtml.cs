using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using myEM.Data;
using myEM.Models;

namespace myEM.Pages.Skills
{
    public class IndexModel : PageModel
    {
        private readonly CompanyContext _context;

        public IndexModel(CompanyContext context)
        {
            _context = context;
        }

        public IList<Skill> Skill { get;set; }

        public async Task OnGetAsync()
        {
            Skill = await _context.Skills.ToListAsync();
        }
    }
}
