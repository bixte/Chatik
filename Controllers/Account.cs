using Chatik.DataModels;
using Chatik.Models.Finders;
using Chatik.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Chatik.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly SignInManager<User> SignInManager;
        private readonly UserManager<User> UserManager;


        public Account(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }


        [Route("Registration")]
        [HttpPost]
        public async Task<ActionResult> Registration(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(viewModel.Name);
                if (user == null)
                {
                    user = new(viewModel.Name) { UserName = viewModel.Name };
                    var result = await UserManager.CreateAsync(user, viewModel.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, true);
                        return Ok();
                    }
                    else
                        return BadRequest();
                }
                else
                    return BadRequest("данный пользователь уже существует");

            }
            return BadRequest("не прошло валидацию");


        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> Login(UserViewModel viewModel)
        {
            var user = await UserManager.FindByNameAsync(viewModel.Name);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(user, viewModel.Password, true, false);
                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(new { error = "неверный логин или пароль" });

            }
            return BadRequest(new { error = "пользователь не найден" });
        }

        [HttpGet]
        public async Task<ActionResult> GetProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var httpUserFinder = new HttpUserFinder(HttpContext, UserManager);
                var user = await httpUserFinder.FindAsync();
                return Ok(user);
            }
            return NotFound("требуется авторизоваться");
        }

    }
}
