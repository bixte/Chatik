using Chatik.DataModels;

namespace Chatik.Models.Validators
{
    public interface IDialogModelValidator
    {
        public bool Validate(Dialog dialog);
    }
}
