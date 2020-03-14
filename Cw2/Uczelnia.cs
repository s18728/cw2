using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Cw2
{
    [Serializable]
    public class Uczelnia
    {
        public Uczelnia() { }
        public Uczelnia(Uczelnia uczelnia)
        {
            this.activeStudies = uczelnia.activeStudies;
            this.studenci = uczelnia.studenci;
            this.author = uczelnia.author;
            this.createdAt = uczelnia.createdAt;
        }

        [XmlAttribute]public string createdAt { get; set; }
        [XmlAttribute] public string author { get; set; }
        public HashSet<Student> studenci { get; set; }
        public List<ActiveStudies> activeStudies { get; set; }
    }
}
