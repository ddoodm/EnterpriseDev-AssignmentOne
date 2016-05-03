using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Users
{
    public abstract class EnetCareUser
    {
        public int ID { get; private set; }
        public string Name { get; private set; }

        /// <summary>
        /// The User's position (title), ie "Site Engineer"
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// The page to which the User is directed upon log-in
        /// </summary>
        public abstract string HomePage { get; }

        protected EnetCareUser(int ID, string name)
        {
            this.ID = ID;
            this.Name = name;
        }
    }
}
