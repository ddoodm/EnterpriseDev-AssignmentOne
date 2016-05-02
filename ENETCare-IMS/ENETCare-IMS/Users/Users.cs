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
            users = new List<User>();
        }

        public void Add(User user)
        {
            users.Add(user);
        }

        public void Add(Users newUsers)
        {
            foreach (User user in newUsers)
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

        public User GetUserByID(int ID)
        {
            if (ID == 0)
                throw new IndexOutOfRangeException("ENETCare data is 1-indexed, but an index of 0 was requested.");
            return users.First<User>(
                user => user.ID == ID);
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

        public List<SiteEngineer> GetSiteEngineers()
        {
            List<User> siteEngineerUsers = users.Where(user => user is SiteEngineer).ToList();
            List<SiteEngineer> siteEngineers = new List<SiteEngineer>();
            foreach (User user in siteEngineerUsers)
            {
                siteEngineers.Add(user as SiteEngineer);
            }

            return siteEngineers;
        }

        public List<Manager> GetManagers()
        {
            List<User> managerUsers = users.Where(user => user is Manager).ToList();
            List<Manager> managers = new List<Manager>();
            foreach (User user in managers)
            {
                managers.Add(user as Manager);
            }

            return managers;
        }
    }
}
