using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2
{
    public class Uczelnia
    {
        [XmlAttribute] public string createdAt;
        [XmlAttribute] public string author;
        public HashSet<Student> studenci;
        public List<ActiveStudies> activeStudies;
    }
}
