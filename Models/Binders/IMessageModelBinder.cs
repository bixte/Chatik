using Chatik.DataModels;

namespace Chatik.Models.Binders
{
    public interface IMessageModelBinder
    {
        public Message Bind(int dialogId, User sender, string message);
    }
}
