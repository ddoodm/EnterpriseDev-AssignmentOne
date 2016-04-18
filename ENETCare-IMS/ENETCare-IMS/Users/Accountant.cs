using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Users
{
    public class Accountant : User
    {
        public Accountant(
            string name,
            string username,
            string plaintextPassword,
            accType type)
            : base (name,username,plaintextPassword, type)
        {

        }

    }
}
