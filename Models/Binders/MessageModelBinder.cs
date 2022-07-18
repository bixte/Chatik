using Chatik.DataModels;
using System;

namespace Chatik.Models.Binders
{
    public class MessageModelBinder : IMessageModelBinder
    {
        public Message Bind(int dialogId,User sender, string message)
        {
            if (sender != null && !string.IsNullOrWhiteSpace(message))
            {
                return new Message
                {
                    DialogId = dialogId,
                    DateTime = DateTime.Now.ToString("G"),
                    User = sender,
                    Text = message
                };
            }
            else
                throw new Exception("ошибка валидации");
        }
    }
}
