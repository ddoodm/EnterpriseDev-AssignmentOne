using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Users
{
    public class SiteEngineer : User, ILocalizedUser
    {
        public District District { get; private set; }
        public decimal MaxApprovableLabour { get; private set; }
        public decimal MaxApprovableCost { get; private set; }

        public SiteEngineer(
            string name,
            string username,
            string plaintextPassword,
            accType type,
            District district,
            decimal maxApprovableLabour,
            decimal maxApprovableCost)
            : base(name, username, plaintextPassword, type)
        {
            this.District = district;
            this.MaxApprovableLabour = maxApprovableLabour;
            this.MaxApprovableCost = maxApprovableCost;
        }

    }
}
