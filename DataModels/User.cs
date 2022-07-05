using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Chatik.DataModels
{
    public class User : IdentityUser
    {
        public string NickName { get; set; }
        public List<Dialog> Dialogs { get; set; }

        public User()  { }
        public User(string name)
        {
            UserName = name;
        }
    }

}
