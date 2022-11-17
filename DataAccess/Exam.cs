using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    [MetadataType(typeof(Exam))]
    public partial class ExamSchedule
    {

    }
    public class Exam
    {
        [Display(Name = "ClassId")]
        [Required(ErrorMessage = "The ClassId field is required")]
        public string ClassId { get; set; }
        [Display(Name = "SubjectID")]
        [Required(ErrorMessage = "The SubjectID field is required")]
        public string SubjectID { get; set; }
        [Display(Name = "Examday")]
        [Required(ErrorMessage = "The Examday field is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Examday { get; set; }
        [Display(Name = "TimeIn")]
        [Required(ErrorMessage = "The TimeIn field is required")]
        public System.TimeSpan TimeIn { get; set; }
        [Display(Name = "TimeOut")]
        [Required(ErrorMessage = "The TimeOut field is required")]
        public System.TimeSpan TimeOut { get; set; }
        [Display(Name = "Note")]
        [Required(ErrorMessage = "The Note field is required")]
        public string Note { get; set; }
        [Display(Name = "RoomID")]
        [Required(ErrorMessage = "The RoomID field is required")]
        public int RoomID { get; set; }
        
       
       
    }
}
