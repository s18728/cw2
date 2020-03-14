﻿using System;
using System.Collections.Generic;

namespace Cw2
{
    public class OwnComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer.InvariantCultureIgnoreCase.Equals($"{x.fname} {x.lname} {x.indexNumber}", $"{y.fname} {y.lname} {y.indexNumber}");
        }

        public int GetHashCode(Student obj)
        {
            return StringComparer.CurrentCultureIgnoreCase.GetHashCode($"{obj.fname} {obj.lname} {obj.indexNumber}");
        }
    }
}
