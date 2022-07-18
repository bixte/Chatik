using Chatik.DataModels;

namespace Chatik.Models.Binders
{
    public interface IDialogModelBinder
    {
        public Dialog Create(User currentUser, User interloscutor);
    }
}
