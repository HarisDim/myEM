using System;
using System.ComponentModel.DataAnnotations;

namespace myEM.Models
{
    public class Employee
    {
        
        public int ID { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public string Details { get; set; }

        public string? Skill_IDs { get; set; }

    }
}
