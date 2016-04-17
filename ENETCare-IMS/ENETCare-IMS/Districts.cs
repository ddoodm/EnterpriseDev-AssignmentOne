using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS
{
    public static class Districts
    {
        private static List<District> districts = new List<District>();

        public static void PopulateDistricts()
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

        public static int Count
        {
            get { return districts.Count; }
        }

        public static District GetDistrictByID(int id)
        {
            return districts.First<District>(d => d.ID == id);
        }

        public static bool IsPopulated()
        {
            return districts.Count == 0;
        }

        public static List<District> DistrictList
        {
            get
            { return districts; }
        }
    }
}
