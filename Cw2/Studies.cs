using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2
{
    public class Studies
    {
        public string name;
        public string mode;

        public bool Equals(Studies studies2)
        {
            if (studies2.name.Equals(this.name)) return true;
            else return false;
        }
    }
}
