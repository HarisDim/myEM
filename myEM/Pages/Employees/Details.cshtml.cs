using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using myEM.Data;
using myEM.Models;

namespace myEM.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly CompanyContext _context;

        public DetailsModel(CompanyContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }
        public List<Skill> Skills { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Skills = new List<Skill>();
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

            if (!string.IsNullOrEmpty(Employee.Skill_IDs))  //if the employee has skill ids
            {
                if (Employee.Skill_IDs.Contains(",")) //if they are more than one
                {
                    foreach (var _id in Employee.Skill_IDs.Split(',')) //split the ','
                    {
                        var skill =  _context.Skills.AsNoTracking().FirstOrDefault(m => m.SkillID == Convert.ToInt32(_id));
                        if (skill != null) 
                        {
                            Skills.Add(skill);
                        }
                    }
                }
                else
                {
                    if (Employee.Skill_IDs.Count().Equals(1))
                    {
                        var skill = _context.Skills.AsNoTracking().SingleOrDefault(m => m.SkillID == Convert.ToInt32(Employee.Skill_IDs));
                        if (skill != null)
                        {
                            Skills.Add(skill);
                        }
                    }
                }
            }
            return Page();
        }
    }
}
