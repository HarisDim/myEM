using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myEM.Data;
using myEM.Models;

namespace myEM.Pages.Employees
{
    public class EditModel : PageModel
    {
    
        private readonly CompanyContext _context;

        public EditModel(CompanyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }
        public IList<virtual_Skill> Skills { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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


            Skills = _context.Skills //get all of the skills for the current employee
                .Select(x => new virtual_Skill
                {
                    DateOfCreation = x.DateOfCreation,
                    SkillID = x.SkillID,
                    Details = x.Details,
                    SkillName = x.SkillName,
                    IsChecked = CheckIfIsChecked(x, Employee) //check if the current employee has this skill
                })
                .ToList();

            return Page(); 
        }

        public static bool CheckIfIsChecked(Skill x, Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Skill_IDs))
            { return false; }

            if (employee.Skill_IDs.Contains(","))
            {
                List<int> skills = employee.Skill_IDs.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                if (skills.Contains(x.SkillID)) return true;
                else return false;
            }
            else {
                if (Convert.ToInt32(employee.Skill_IDs) == x.SkillID) return true;
                else return false;
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            var employeeToUpdate = await _context.Employees.FindAsync(id);
            
            if (employeeToUpdate == null)
            {
                return NotFound();
            }

            #region get the checked skill from the query string in the url 
            List<int> skillids = new List<int>();

            
            if (!string.IsNullOrEmpty(employeeToUpdate.Skill_IDs)) //gets old skill id's
            {
                foreach (var t in employeeToUpdate.Skill_IDs.Split(",")) //splits them 
                {
                    skillids.Add(Convert.ToInt32(t)); //adds them to the current list
                }
            }

            
            foreach (var t in Request.Query) //get all query strings and add them to the current list or remove them
            {
                if (t.Key.StartsWith("k")) 
                { 
                    if (Convert.ToBoolean(t.Value) == true)     
                        skillids.Add(Convert.ToInt32(Convert.ToString(t.Key).Replace("k", string.Empty)));  
                    else  
                        skillids.Remove(Convert.ToInt32(Convert.ToString(t.Key).Replace("k", string.Empty)));
                }
            }
            
            employeeToUpdate.Skill_IDs = string.Join(",", skillids); //join the skillids and add the new value to the employee
            #endregion


            if (await TryUpdateModelAsync<Employee>(employeeToUpdate, "Employee",  s => s.FirstName, s => s.LastName, s => s.HiringDate, s =>s.Details, s =>s.Skill_IDs))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public class virtual_Skill //create virtual class parameter for skill 
        {
            public int SkillID { get; set; }
            public string SkillName { get; set; }
            public string? Details { get; set; }
            public DateTime DateOfCreation { get; set; }
            public bool IsChecked { get; set; } = false;
        }
    }
}
