using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Users
{


    public abstract class User
    {
        public enum accType { none = -1, SiteEngineer, Manager, Accountant };

        //basic User Class
        public string Name { get; protected set; }
        public string Username { get; protected set; }
        public accType Type { get; protected set; }

        protected User(string name, string username, string plaintextPassword, accType type)
        {
            this.Name = name;
            this.Username = username;
            this.Type = type;

            /* TODO: Encrypt and store the password.
               Shall we try Salted Password Hashing?
               MD5s aren't cool anymore. */

        }


    }
}
