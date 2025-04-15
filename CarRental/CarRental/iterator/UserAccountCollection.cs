using System.Collections;
using System.Collections.Generic;

namespace CarRental.iterator
{
    public class UserAccount
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public bool Enabled { get; set; }
    }

    public class UserAccountCollection : IEnumerable<UserAccount>
    {
        private List<UserAccount> _userAccounts = new List<UserAccount>();

        public void AddUserAccount(UserAccount userAccount)
        {
            _userAccounts.Add(userAccount);
        }

        public IEnumerator<UserAccount> GetEnumerator()
        {
            return _userAccounts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
