using System;

namespace Chatik.DataModels
{
    public class Message
    {
        public int Id { get; set; }
        public int DialogId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public string DateTime { get; set; }
    }
}
