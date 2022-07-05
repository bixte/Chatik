using Chatik.DataModels;
using Chatik.Models.Finder;
using Chatik.Models.Finders;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chatik.Models.Senders
{
    public class DialogFormatter : IFormatter
    {
        private readonly List<Dialog> dialogs;
        private readonly DialogInterlocutorFinder InterlocutorUserFinder;

        public DialogFormatter(List<Dialog> dialogs, DialogInterlocutorFinder dialogInterlocutorUserFinder)
        {
            this.dialogs = dialogs;
            InterlocutorUserFinder = dialogInterlocutorUserFinder;
        }
        public async Task<List<DialogModelAPI>> FormatteAsync()
        {
            if (dialogs != null)
            {
                List<DialogModelAPI> dialogsApi = new();

                foreach (var dialog in dialogs)
                {
                    var interLocutor = await InterlocutorUserFinder.GetInterlocutorAsync(dialog);
                    dialogsApi.Add(new()
                    {
                        Interlocutor = interLocutor.FirstOrDefault(),
                        LastMessage = dialog.Messages.Last()
                    });
                }
                return dialogsApi;
            }
            return default;
            
        }
    }
}
