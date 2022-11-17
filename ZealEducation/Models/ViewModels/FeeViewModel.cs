using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZealEducation.Models.ViewModels
{
    public class FeeViewModel
    {
        // Student
        public string StudentID { get; set; }

        public string StudentName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public bool Gender { get; set; }

        public string Email { get; set; }

        // Class
        public string ClassID { get; set; }
        public string ClassName { get; set; }

        // Subject
        public string SubjectID { get; set; }
        public string SubjectName { get; set; }

        // Fee
        public int FeesID { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<bool> Status { get; set; }

    }
}