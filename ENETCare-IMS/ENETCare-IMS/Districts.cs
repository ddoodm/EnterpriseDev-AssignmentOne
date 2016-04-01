using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS
{
    public class Districts : IReadOnlyList<District>
    {
        private List<District> districts;

        public Districts()
        {
            districts = new List<District>();

            // TODO: This will retrieve ENETCare's operating districts from
            // the database. These are temporary placeholders.
            districts.Add(new District("Urban Indonesia"));
            districts.Add(new District("Rural Indonesia"));
            districts.Add(new District("Urban Papua New Guinea"));
            districts.Add(new District("Rural Papua New Guinea"));
            districts.Add(new District("Sydney"));
            districts.Add(new District("Rural New South Wales"));
        }

        public int Count
        {
            get { return districts.Count; }
        }

        public District this[int i]
        {
            get { return districts[i]; }
        }

        public IEnumerator<District> GetEnumerator()
        {
            return districts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
