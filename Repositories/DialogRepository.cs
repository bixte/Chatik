using Chatik.DataModels;
using Chatik.Models.Binders;
using Chatik.Models.Finders;
using Chatik.Models.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatik.Repositories
{
    public class DialogsRepository
    {
        private readonly ChatikDbContext ChatikDbContext;
        private readonly UserManager<User> userManager;

        public DialogsRepository(ChatikDbContext chatikDbContext, UserManager<User> userManager)
        {
            ChatikDbContext = chatikDbContext;
            this.userManager = userManager;
        }

        public async Task<IQueryable<object>> GetDialogsAsync(IUserFinder currentUserFinder)
        {
            var currentUser = await currentUserFinder.FindAsync();
            var dialogs = ChatikDbContext.Dialogs
                .Include(d => d.Users.Where(u => u.Id == currentUser.Id))
                .Include(d => d.Messages)
                .Select(d => new
                {
                    idDialog = d.Id,
                    inteloscutor = d.Users.Where(u => u.Id != currentUser.Id)
                                          .Select(u => new { id = u.Id, userName = u.UserName }),
                    messages = d.Messages.OrderBy(m => m.DateTime).Last()
                }).AsNoTracking();

            return dialogs;
        }

        public async Task AddDialogAsync(IUserFinder currentUserFinder,
                                         string idInterloscutor,
                                         IDialogModelValidator dialogModelValidator,
                                         IDialogModelBinder dialogModelBinder)
        {
            var currentUser = await currentUserFinder.FindAsync();
            var interloscutor = await userManager.FindByIdAsync(idInterloscutor);
            if (!await DialogExist(currentUser, interloscutor))
            {
                var dialog = dialogModelBinder.Create(currentUser, interloscutor);
                if (dialogModelValidator.Validate(dialog))
                {
                    await ChatikDbContext.Dialogs.AddAsync(dialog);
                    await ChatikDbContext.SaveChangesAsync();
                }
                else
                    throw new System.Exception("ошибка создания диалога");
            }
            else
                throw new System.Exception("данный диалог уже существует");
        }

        private async Task<bool> DialogExist(User currentUser, User interloscutor)
        {
            await ChatikDbContext.Entry(currentUser)
                                 .Collection(u => u.Dialogs)
                                 .LoadAsync();
            return currentUser.Dialogs.Exists(d => d.Users.Count == 2
                                                   && d.Users.Exists(u => u.Id == interloscutor.Id));
        }
    }
}
