using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2
{
    [XmlType("studies")]
    public class ActiveStudies
    {
        [XmlAttribute]public string name;
        [XmlAttribute]public int numberOfStudents;
    }
}
