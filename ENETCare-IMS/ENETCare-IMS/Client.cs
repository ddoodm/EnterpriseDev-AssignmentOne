using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS
{
    public class Client
    {
        public string Name          { get; private set; }
        public District District    { get; private set; }
        public string Location      { get; private set; }

        public Client(string name, string location, District district)
        {
            this.Name = name;
            this.Location = location;
            this.District = district;
        }
    }
}
