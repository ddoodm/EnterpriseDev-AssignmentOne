﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Interventions
{
    public class InterventionTypes : IReadOnlyList<InterventionType>
    {
        private List<InterventionType> types;

        public InterventionTypes()
        {
            types = new List<InterventionType>();

            // TODO: Remove these test types, and use the database instead
            types.Add(new InterventionType("Supply and Install Portable Toilet", 600.0m, 3));
            types.Add(new InterventionType("Hepatitis Avoidance Training", 350.0m, 7));
            types.Add(new InterventionType("Supply and Install Storm-proof Home Kit", 1000.0m, 9));
        }

        public int Count
        {
            get { return types.Count; }
        }

        public InterventionType this[int i]
        {
            get { return types[i]; }
        }

        public IEnumerator<InterventionType> GetEnumerator()
        {
            return types.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<InterventionType> Types
        {
            get
            {
                return types;
            }
        }
    }
}
