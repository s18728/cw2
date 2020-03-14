using System;
using System.Xml.Serialization;

namespace Cw2
{
    [Serializable]
    [XmlType("studies")]
    public class ActiveStudies
    {
        [XmlAttribute]public string name { get; set; }
        [XmlAttribute]public int numberOfStudents { get; set; }
    }
}
