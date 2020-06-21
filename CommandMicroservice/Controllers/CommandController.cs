using CommandMicroservice.CommandSender;
using Microsoft.AspNetCore.Mvc;

namespace CommandMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private ICommandSender sender;
        public CommandController(ICommandSender sender)
        {
            this.sender = sender;
        }

        [HttpGet("")]
        public ActionResult SendCommand()
        {
            sender.SendCommand(15);
            return Ok();
        }
    }
}
