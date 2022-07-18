using Chatik.DataModels;
using Chatik.Models.Binders;
using Chatik.Models.Finders;
using Chatik.Models.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Chatik.Repositories
{
    public class MessagesRepository
    {
        private readonly ChatikDbContext chatikDbContext;

        public MessagesRepository(ChatikDbContext chatikDbContext)
        {
            this.chatikDbContext = chatikDbContext;
        }
        public async Task<object> GetMessagesAsync(int dialogId, IUserFinder currentUserFinder, IDialogAccessValidator dialogValidator)
        {

            var currentUser = await currentUserFinder.FindAsync();
            var dialog = chatikDbContext.Dialogs.Where(d => d.Id == dialogId)
                                    .Include(d => d.Users).IgnoreAutoIncludes()
                                    .Include(d => d.Messages.OrderBy(m => m.DateTime)).ThenInclude(m => m.User).IgnoreAutoIncludes()
                                    .Select(d => new
                                    {
                                        members = d.Users.Select(u => new
                                        {
                                            userId = u.Id,
                                            userName = u.UserName,
                                            isCurrent = currentUser.Id == u.Id
                                        }),
                                        messages = d.Messages.Select(m => new
                                        {
                                            idMessage = m.Id,
                                            user = m.User.UserName,
                                            text = m.Text,
                                            dateTime = m.DateTime
                                        }),

                                    });
            return dialog;
        }

        public async Task SendMessageAsync(int dialogId,
                                      string message,
                                      IUserFinder userFinder,
                                      IDialogModelValidator dialogModelValidator,
                                      IDialogAccessValidator dialogAccessValidator,
                                      IMessageModelBinder messageModelBinder)
        {
            var dialog = await chatikDbContext.Dialogs.Where(d => d.Id == dialogId)
                                                      .Include(d => d.Users)
                                                      .IgnoreAutoIncludes()
                                                      .AsNoTracking()
                                                      .FirstOrDefaultAsync();
            if (dialogModelValidator.Validate(dialog))
            {
                var user = await userFinder.FindAsync();
                if (dialogAccessValidator.Validate(dialog, user))
                {
                    var messageModel = messageModelBinder.Bind(dialogId, user, message);
                    chatikDbContext.Messages.Add(messageModel);
                    await chatikDbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("не удалось получить доступ к диалогу");

            }
            else
                throw new Exception("диалог не найден");

        }
    }
}
