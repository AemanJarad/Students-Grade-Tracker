using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsGradeTracker
{
    internal class Class1
    {
        public class User
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }   

        public class Student : User
        {
            public int StudentID { get; set; }
            public int StudentClass { get; set; }
        }

        public class Teacher : User
        {
            public int TeacherID { get; set; }
            public string Department { get; set; }
        }

        public class Admin : User
        {
            public int AdminID { get; set; }
        }

    }
}
