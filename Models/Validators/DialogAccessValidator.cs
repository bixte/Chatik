using Chatik.DataModels;

namespace Chatik.Models.Validators
{

    public class DialogAccessValidator : IDialogAccessValidator
    {
        public bool Validate(Dialog dialog, User user) =>dialog!= null && user !=null && dialog.Users.Exists(u => u.UserName == user.UserName);
    }
}
