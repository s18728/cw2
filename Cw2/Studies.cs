using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Cw2
{
    [Serializable]
    public class Studies
    {
        public string name { get; set; }
        public string mode { get; set; }

        [XmlIgnore][JsonIgnore]public int numberOfStudents{ get; set; }

        public Studies()
        {
            this.numberOfStudents = 1;
        }
}
}
