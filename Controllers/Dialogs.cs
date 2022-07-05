using Chatik.DataModels;
using Chatik.Models.Finder;
using Chatik.Models.Senders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chatik.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Dialogs : ControllerBase
    {
        public ChatikDbContext ChatikDbContext { get; }
        public UserManager<User> UserManager { get; }

        public Dialogs(ChatikDbContext chatikDbContext, UserManager<User> userManager)
        {
            ChatikDbContext = chatikDbContext;
            UserManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetDialogs()
        {
            await ChatikDbContext.Dialogs.LoadAsync();
            await ChatikDbContext.Messages.LoadAsync();

            var httpUserFinder = new HttpUserFinder(HttpContext, UserManager);
            var userCurrent = await httpUserFinder.GetUserAsync();
            DialogFormatter dialogFormatter = new(userCurrent.Dialogs, new(httpUserFinder));
            var dialogAPI = await dialogFormatter.FormatteAsync();
            return new ObjectResult(dialogAPI);
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<ActionResult> CreateDialog(string id)
        {
            var httpUserFinder = new HttpUserFinder(HttpContext, UserManager);
            var userCurrent = await httpUserFinder.GetUserAsync();

            var interlocutor = await UserManager.FindByIdAsync(id);
            
            await ChatikDbContext.Dialogs.AddAsync(new Dialog
            {
                Users = new List<User>() { userCurrent, interlocutor}
            });
            var result = await ChatikDbContext.SaveChangesAsync();
            if (result > 0)
                return Ok();
            else
                return BadRequest();
        }
    }

}
