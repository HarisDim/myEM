using System;
using System.ComponentModel.DataAnnotations;

namespace myEM.Models
{
    public class Skill
    {
        public int SkillID { get; set; }
        [Required]
        [Display(Name = "Skill's Name")]
        public string SkillName { get; set; }
        [Required]
        [Display(Name = "Skill's Details")]
        public string? Details { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
