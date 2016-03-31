using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS
{
    public class District
    {
        public string Name { get; private set; }

        public District(string name)
        {
            this.Name = name;
        }
    }
}
