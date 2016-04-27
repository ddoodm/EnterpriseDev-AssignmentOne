using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS
{
    public class Districts
    {
        private ENETCareDAO application;

        private static List<District> districts = new List<District>();

        public Districts(ENETCareDAO application)
        {
            this.application = application;

            PopulateDistricts();
        }

        private void PopulateDistricts()
        {
            districts = new List<District>();

            // TODO: This will retrieve ENETCare's operating districts from
            // the database. These are temporary placeholders.
            districts.Add(new District(1, "Urban Indonesia"));
            districts.Add(new District(2, "Rural Indonesia"));
            districts.Add(new District(3, "Urban Papua New Guinea"));
            districts.Add(new District(4, "Rural Papua New Guinea"));
            districts.Add(new District(5, "Sydney"));
            districts.Add(new District(6, "Rural New South Wales"));
        }

        public int Count
        {
            get { return districts.Count; }
        }

        public District GetDistrictByID(int id)
        {
            if (id == 0)
                throw new IndexOutOfRangeException("ENETCare data is 1-indexed, but an index of 0 was requested.");
            return districts.First<District>(d => d.ID == id);
        }

        public List<District> GetListCopy()
        {
            return new List<District>(districts);
        }
    }
}
