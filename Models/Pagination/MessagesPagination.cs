using Chatik.DataModels;
using System.Collections.Generic;

namespace Chatik.Models.Pagination
{
    public class MessagesPagination
    {
        private readonly List<Message> messages;

        public MessagesPagination(List<Message> messages, int messagePage)
        {
            this.messages = messages;
            MessagePage = messagePage;
        }

        public int MessagePage { get; }

        /*public List<Message> GetMessages()
        {

        }*/
    }
}
