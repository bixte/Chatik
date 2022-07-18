using Chatik.DataModels;
using Chatik.Models.Binders;
using Chatik.Models.Finders;
using Chatik.Models.Paginations;
using Chatik.Models.Validators;
using Chatik.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Chatik.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Dialogs : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly DialogsRepository dialogsRepository;
        private readonly MessagesRepository messagesRepository;

        public Dialogs(UserManager<User> userManager, DialogsRepository dialogsRepository, MessagesRepository messagesRepository)
        {

            this.userManager = userManager;
            this.dialogsRepository = dialogsRepository;
            this.messagesRepository = messagesRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetDialogs([FromQuery] PaginationOption option)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dialogs = await dialogsRepository.GetDialogsAsync(new HttpUserFinder(HttpContext, userManager));

                var dialogPaginated = await PaginatedList<object>.CreateAsync(dialogs, option.CurrentPage, option.PageSize);
                return Ok(dialogPaginated);
            }
            return Unauthorized();
        }


        [HttpPost()]
        public async Task<ActionResult> CreateDialog([FromQuery] string idUser)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    await dialogsRepository.AddDialogAsync(new HttpUserFinder(HttpContext, userManager),
                                                       idUser,
                                                       new GeneralDialogValidator(),
                                                       new GeneralDialogBinder());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }



        [HttpGet("{dialogId}")]
        public async Task<ActionResult> GetMessagesAsync(int dialogId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dialog = await messagesRepository.GetMessagesAsync(dialogId,
                                                              new HttpUserFinder(HttpContext, userManager),
                                                              new DialogAccessValidator());
                return Ok(dialog);
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpPost("{dialogId}")]
        public async Task<ActionResult> SendMessageAsync(int dialogId, string message)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    await messagesRepository.SendMessageAsync(dialogId,
                                                              message,
                                                              new HttpUserFinder(HttpContext, userManager),
                                                              new GeneralDialogValidator(),
                                                              new DialogAccessValidator(),
                                                              new MessageModelBinder());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }


    }

}
