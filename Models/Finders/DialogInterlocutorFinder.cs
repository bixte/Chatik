using Chatik.DataModels;
using Chatik.Models.Finder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatik.Models.Finders
{
    public class DialogInterlocutorFinder : IFinder
    {
        private readonly HttpUserFinder httpUserFinder;

        public DialogInterlocutorFinder(HttpUserFinder httpUserFinder)
        {
            this.httpUserFinder = httpUserFinder;
        }

        public async Task<IEnumerable<User>> GetInterlocutorAsync(Dialog dialog)
        {
            var currentUser = await httpUserFinder.GetUserAsync();
            return dialog.Users.Where(u => u.UserName != currentUser.UserName);
        }
    }
}
