using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2
{
    public class Studies
    {
        public string name;
        public string mode;

        [XmlIgnore] public int numberOfStudents = 1;
    }
}
