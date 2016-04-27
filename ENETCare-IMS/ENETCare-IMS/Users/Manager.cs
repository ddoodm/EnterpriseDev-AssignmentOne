using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Users
{
    public class Manager : User, ILocalizedUser
    {
        private const string TITLE = "Manager";

        public District District { get; private set; }
        public decimal MaxApprovableLabour { get; private set; }
        public decimal MaxApprovableCost { get; private set; }

        public override string Title
        {
            get
            {
                return TITLE;
            }
        }

        public Manager(
            int ID,
            string name,
            string username,
            string plaintextPassword,
            District district,
            decimal maxApprovableLabour,
            decimal maxApprovableCost)
            : base(ID, name, username, plaintextPassword)
        {
            this.District = district;
            this.MaxApprovableLabour = maxApprovableLabour;
            this.MaxApprovableCost = maxApprovableCost;
        }
    }
}
