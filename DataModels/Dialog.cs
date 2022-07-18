using System.Collections.Generic;

namespace Chatik.DataModels
{
    public class Dialog
    {
        public int Id { get; set; }
        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }

    }
}
