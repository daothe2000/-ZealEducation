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
    
    public partial class ClassSubject
    {
        public int ID { get; set; }
        public string ClassID { get; set; }
        public string SubjectID { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
