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
            districts.Add(new District(0, "Urban Indonesia"));
            districts.Add(new District(1, "Rural Indonesia"));
            districts.Add(new District(2, "Urban Papua New Guinea"));
            districts.Add(new District(3, "Rural Papua New Guinea"));
            districts.Add(new District(4, "Sydney"));
            districts.Add(new District(5, "Rural New South Wales"));
        }

        public int Count
        {
            get { return districts.Count; }
        }

        public District GetDistrictByID(int id)
        {
            return districts.First<District>(d => d.ID == id);
        }

        public List<District> GetListCopy()
        {
            return new List<District>(districts);
        }
    }
}
