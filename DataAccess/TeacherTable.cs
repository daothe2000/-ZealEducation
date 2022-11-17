using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    [MetadataType(typeof(TeacherTable))]
    public partial class Teacher
    {

    }
    public class TeacherTable
    {
        [Display(Name = "TeacherID")]
        [Required(ErrorMessage = "The TeacherID field is required")]
        [MinLength(1, ErrorMessage = "The TeacherID field is required")]
        public string TeacherID { get; set; }
        [Display(Name = "TeacherName")]
        [Required(ErrorMessage = "The TeacherName field is required")]
        [MinLength(1, ErrorMessage = "The TeacherName field is required")]
        public string TeacherName { get; set; }
        [Required(ErrorMessage = "The Phone field is required")]
        [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Not a valid phone number")]

        public string Phone { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "The Address field is required")]
        [MinLength(1, ErrorMessage = "The Address field is required")]
        public string Address { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "The Password field is required")]
        [MinLength(1, ErrorMessage = "The Password field is required")]
        public string Password { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The Email field is required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
                   ErrorMessage = "Entered Email format is not valid.")]
        public string Email { get; set; }
        [Display(Name = "Image")]
        [Required(ErrorMessage = "The Image field is required")]
        public string Image { get; set; }
        [Display(Name = "Birthday")]
        [Required(ErrorMessage = "The Birthday field is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public bool Status { get; set; }
        
        public string Nation { get; set; }
       
        public string Degree { get; set; }
        [Display(Name = "FacultyID")]
        [Required(ErrorMessage = "The FacultyID field is required")]
        public string FacultyID { get; set; }
    }
}
