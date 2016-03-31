using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS
{
    public class Client
    {
        private District district;
        private string name;
        private string location;

        public Client(string name, string location, District district)
        {
            this.name = name;
            this.location = location;
            this.district = district;
        }
    }
}
