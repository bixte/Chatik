using Chatik.DataModels;
using Chatik.Models.Finder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatik.Models.Finders
{
    public class DialogInterlocutorFinder : IFinder<Task<IEnumerable<User>>>
    {
        private readonly HttpUserFinder httpUserFinder;

        public DialogInterlocutorFinder(HttpUserFinder httpUserFinder)
        {
            this.httpUserFinder = httpUserFinder;
        }

        public Task<IEnumerable<User>> FindAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetInterlocutorAsync(Dialog dialog)
        {
            var currentUser = await httpUserFinder.FindAsync();
            return dialog.Users.Where(u => u.UserName != currentUser.UserName);
        }
    }
}
