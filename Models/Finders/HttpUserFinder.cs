using Chatik.DataModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chatik.Models.Finders
{
    public class HttpUserFinder : IUserFinder
    {
        private readonly UserManager<User> UserManager;
        private readonly ClaimsPrincipal User;

        public HttpUserFinder(HttpContext httpContext, UserManager<User> userManager)
        {
            User = httpContext.User;
            UserManager = userManager;
        }


        public async Task<User> FindAsync() => await UserManager.FindByNameAsync(User.Identity.Name);
    }
}
