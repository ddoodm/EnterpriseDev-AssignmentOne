﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Users
{
    public class Manager : User
    {
        public District District { get; private set; }
        public decimal MaxApprovableLabour { get; private set; }
        public decimal MaxApprovableCost { get; private set; }

        public Manager(
            string name,
            string username,
            string plaintextPassword,
            District district,
            decimal maxApprovableLabour,
            decimal maxApprovableCost)
            : base(name, username, plaintextPassword)
        {
            this.District = district;
            this.MaxApprovableLabour = maxApprovableLabour;
            this.MaxApprovableCost = maxApprovableCost;
        }
    }
}
