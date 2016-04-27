using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Users
{
    public class Accountant : User
    {
        private const string TITLE = "Accountant";

        public override string Title
        {
            get
            {
                return TITLE;
            }
        }

        public Accountant(
            int ID,
            string name,
            string username,
            string plaintextPassword)
            : base (ID, name,username,plaintextPassword)
        {

        }
    }
}
