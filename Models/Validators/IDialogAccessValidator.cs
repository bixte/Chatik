using Chatik.DataModels;

namespace Chatik.Models.Validators
{
    public interface IDialogAccessValidator
    {
        public bool Validate(Dialog dialog, User currentUser);
    }
}
