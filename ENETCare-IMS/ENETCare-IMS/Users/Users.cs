using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Users
{
    public class Users : IReadOnlyList<User>
    {
        private ENETCareDAO application;
        private List<User> users;

        public Users(ENETCareDAO application)
        {
            this.application = application;
            if (application.Users == null)
            {
                users = new List<User>();
            }
            else
            {
                users = application.Users.GetUsers();
            }
        }

        /// <summary>
        /// Populates the list of users with users
        /// </summary>
        public void PopulateUsersList()
        {
            // Placeholder data:
            Districts districts = application.Districts;
            Add(new Manager(1, "Daum Park", "daum", "1234", districts.GetDistrictByID(1), 8, 1024));
            Add(new SiteEngineer(2, "Deinyon Davies", "deinyon", "1234", districts.GetDistrictByID(1), 9, 900));
            Add(new SiteEngineer(3, "Henry Saal", "henry", "1234", districts.GetDistrictByID(2), 9, 1000));
            Add(new Accountant(4, "Yiannis Chambers", "yiannis", "1234"));
        }

        public void Add(User user)
        {
            users.Add(user);
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

        public User GetUserByID(int id)
        {
            return users.First<User>(c => c.ID == id);
        }

        public IEnumerator<User> GetEnumerator()
        {
            return users.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get { return users.Count; } }

        public User this[int index]
        {
            get
            {
                return users.First<User>(user => user.ID == index);
            }
        }

        public List<User> GetUsers()
        {
            return users;
        }
    }
}
