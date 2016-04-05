using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS
{
    public class District
    {
        public int ID      { get; private set; }
        public string Name { get; private set; }

        public District(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
