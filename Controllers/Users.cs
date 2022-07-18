using Chatik.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Chatik.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly ChatikDbContext chatikDbContext;

        public Users(ChatikDbContext chatikDbContext)
        {
            this.chatikDbContext = chatikDbContext;
        }
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = chatikDbContext.Users;
            return Ok(users);
        }

        [HttpGet]
        [Route("{nickname}")]
        public async Task<ActionResult> Search(string nickname)
        {
            var user = await chatikDbContext.Users.FirstOrDefaultAsync(u => u.UserName == nickname);
            return user == null ? NotFound() : Ok(user);
        }
    }
}
