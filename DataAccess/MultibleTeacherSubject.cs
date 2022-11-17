using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    
    
    class MultibleTeacherSubject
    {
        
        [Required(ErrorMessage = "Please select a faculty")]
        public int FacultyID { get; set; }

        [Required(ErrorMessage = "Please select a teacher")]

        public int TeacherID { get; set; }
        [Required(ErrorMessage = "Please select your subject")]
        public int SubjectID { get; set; }
    }
}
