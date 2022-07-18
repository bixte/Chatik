using Chatik.DataModels;

namespace Chatik.Models.Validators
{
    public class GeneralDialogValidator : IDialogModelValidator
    {
        public bool Validate(Dialog dialog)
        {
            return dialog !=null && dialog.Users.Count >= 2;
        }
    }
}
