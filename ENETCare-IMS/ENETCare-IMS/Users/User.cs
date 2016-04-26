using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Users
{
    public abstract class User
    {
        //basic User Class
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }

        // TODO: Do not store password in plain-text
        public string PlaintextPassword { get; private set; }

        /// <summary>
        /// The User's position (title), ie "Site Engineer"
        /// </summary>
        public abstract string Title { get; }

        protected User(string name, string username, string plaintextPassword)
        {
            this.Name = name;
            this.Username = username;

            /* TODO: Encrypt and store the password.
               Shall we try Salted Password Hashing?
               MD5s aren't cool anymore. */
            this.PlaintextPassword = plaintextPassword;
        }
    }
}
