using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Users
{
    public class Users
    {
        private ENETCareDAO application;
        private List<User> users;

        public Users(ENETCareDAO application)
        {
            this.application = application;

            users = new List<User>();
            PopulateUsersList();
        }

        /// <summary>
        /// Populates the list of users with users
        /// </summary>
        private void PopulateUsersList()
        {
            // Placeholder data:
            Districts districts = application.Districts;
            Add(new Manager(1, "Daum Park", "daum", "1234", districts.GetDistrictByID(1), 8, 1024));
            Add(new SiteEngineer(2, "Deinyon Davies", "deinyon", "1234", districts.GetDistrictByID(2), 9, 900));
            Add(new SiteEngineer(3, "Henry Saal", "henry", "1234", districts.GetDistrictByID(3), 9, 1000));
            Add(new Accountant(4, "Yiannis Chambers", "yiannis", "1234"));
        }

        private void Add(User user)
        {
            users.Add(user);
        }

        public User GetUserById(int ID)
        {
            if (ID == 0)
                throw new IndexOutOfRangeException("ENETCare data is 1-indexed, but an index of 0 was requested.");
            return users.First<User>(
                user => user.ID == ID);
        }

        public User Login(string username, string plaintextPassword)
        {
            var result = from user in users
                         where user.PlaintextPassword == plaintextPassword
                         && user.Username == username
                         select user;

            if (!result.Any<User>())
                return null;
            return result.First<User>();
        }
    }
}
