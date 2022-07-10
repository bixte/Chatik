using Chatik.DataModels;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Chatik.Models
{
    public class DialogModelAPI
    {
        public Message LastMessage { get; set; }
        public User Interlocutor { get; set; }
    }
}
