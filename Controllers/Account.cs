using Chatik.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Chatik.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly SignInManager<User> SignInManager;
        private readonly UserManager<User> UserManager;

        public ILogger<Account> Logger { get; }

        public Account(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<Account> logger)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            Logger = logger;
        }


        [Route("Registration")]
        [HttpPost]
        public async Task<ActionResult> Registration(string name, string password)
        {
            var user = new User(name);
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, true);
                return Ok();
            }
            else
                return BadRequest();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> Login(string name, string password)
        {
            var user = await UserManager.FindByNameAsync(name);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(user, password, true, false);
                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(new { error = "неверный логин или пароль" });
                
            }
            return BadRequest(new { error = "пользователь не найден" });

        }

    }
}
