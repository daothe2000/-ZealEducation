//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public int AttendanceID { get; set; }
        public string StudentID { get; set; }
        public int TimeID { get; set; }
        public int AttendanceTypeID { get; set; }
        public string Note { get; set; }
        public System.TimeSpan timeIn { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual TimeTable TimeTable { get; set; }
        public virtual AttendanceType AttendanceType { get; set; }
    }
}
