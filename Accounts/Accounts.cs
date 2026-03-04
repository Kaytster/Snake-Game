using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Accounts
{
    //Base accounts class storing ID, Username, and Password
    class Accounts
    {
        protected int UserID;
        protected string Username;
        protected string Password;

        public Accounts(int userID, string username, string password)
        {
            UserID = userID;
            Username = username;
            Password = password;
        }
        public int GetID() { return UserID; }
        public string GetUsername() { return Username; }
        public void SetUsername(string newUsername)
        {
            Username = newUsername;
        }
        public string GetPassword() { return Password; }
        public void SetPassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}
