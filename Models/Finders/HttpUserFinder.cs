using Chatik.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chatik.Models.Finder
{
    public class HttpUserFinder : IFinder<Task<User>>
    {
        private readonly UserManager<User> UserManager;
        private readonly ClaimsPrincipal User;

        public HttpUserFinder(HttpContext httpContext, UserManager<User> userManager)
        {
            UserManager = userManager;
            User = httpContext.User;
        }


        public async Task<User> FindAsync() => await UserManager.FindByNameAsync(User.Identity.Name);

    }
}
