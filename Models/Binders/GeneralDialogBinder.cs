using Chatik.DataModels;
using System.Collections.Generic;

namespace Chatik.Models.Binders
{
    public class GeneralDialogBinder : IDialogModelBinder
    {
        public Dialog Create(User currentUser, User interloscutor)
        {
            if (currentUser != null && interloscutor != null)
            {
                return new Dialog
                {
                    Users = new List<User>() { currentUser, interloscutor }
                };
            }
            else
                throw new System.Exception("ошибка биндинга");

        }
    }
}
