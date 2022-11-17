using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    [MetadataType(typeof(Parents))]
    public partial class Parent
    {

    }
    class Parents
    {
        [Display(Name = "ParentName")]
        [Required(ErrorMessage = "The ParentName field is required")]
        [MinLength(1, ErrorMessage = "The ParentName field is required")]
        public string ParentName { get; set; }
        [Display(Name = "Phone")]
        [Required(ErrorMessage = "The Phone field is required")]
        [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "The Address field is required")]
        [MinLength(1, ErrorMessage = "The Address field is required")]
        public string Address { get; set; }
        
        public bool Gender { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "The Password field is required")]
        [MinLength(1, ErrorMessage = "The Password field is required")]
        public string Password { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The Email field is required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
                   ErrorMessage = "Entered Email format is not valid.")]
        public string Email { get; set; }
    }
}
