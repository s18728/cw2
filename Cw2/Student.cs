using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2
{
    public class Student
    {
         public string indexNumber;
         public string fname;
         public string lname;
         public string birthdate;
         public string email;
         public string mothersName;
         public string fathersName;
         public Studies studies;

        public bool Equals(Student std2)
        {
            if (std2.indexNumber == this.indexNumber) return true;
            else return false;
        }
    }


}
