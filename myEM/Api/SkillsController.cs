using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myEM.Data;
using myEM.Models;

namespace myEM.Api
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly CompanyContext _context;

        public SkillsController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(Duration = 600)]//cache json for 1 minute
        public List<Skill> GetSkills()
        {
            IQueryable<Skill> SkillsIQuery = from s in _context.Skills
                                                   select s;
            return SkillsIQuery.ToList();
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<bool> CreateNewSkill(Skill newskill)
        {
            try
            {
                if (await TryUpdateModelAsync<Skill>(
                    newskill,
                    "skill",
                    s => s.SkillName, s => s.Details))
                {
                    _context.Skills.Add(newskill);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<bool> UpdateSkill(Skill updateskill)
        {
            try
            {
                var skillToUpdate = await _context.Skills.FindAsync(updateskill.SkillID);

                if (skillToUpdate == null)
                {
                    Console.WriteLine("Skill not found");
                    return false;
                }
                skillToUpdate.SkillName = updateskill.SkillName;
                skillToUpdate.Details = updateskill.Details;
                if (await TryUpdateModelAsync<Skill>(updateskill, "Skill",
                    s => s.SkillName,
                    s => s.Details))
                {
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Delete(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("ID cannot be null");
                return false;
            }

            var Skill = await _context.Skills.FindAsync(id);

            if (Skill == null)
            {
                Console.WriteLine("Skill not found");
                return false;
            }

            try
            {
                _context.Skills.Remove(Skill);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
