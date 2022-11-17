using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZealEducation.Models
{
    public class Login
    {

        public enum UserType
        {
            Admin,
            Teacher,
            Student
        };

        public string username { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        public UserType usertype { get; set; }
    }
}