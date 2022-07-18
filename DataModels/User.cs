using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Chatik.DataModels
{
    public class User : IdentityUser
    {
        public List<Dialog> Dialogs { get; set; }

        public User() { }
        public User(string name)
        {
            UserName = name;
        }

        public override bool Equals(object obj)
        {
            if (obj is User user)
                return Id == user.Id;
            else
                throw new Exception("ошибка сравнения двух разных типов");
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
