using System;
using CommandMicroservice.CommandSender;
using Microsoft.AspNetCore.Mvc;
using SharedModels;
using SharedModels.Commands;

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
        
        [HttpPost("decreasewaterlevel")]
        public ActionResult DecreaseWaterLevel(DecreaseWaterLevel command)
        {
            sender.SendCommand(command);
            return Ok();
        }

        [HttpPost("increasewaterlevel")]
        public ActionResult IncreaseWaterLevel(IncreaseWaterLevel command)
        {
            sender.SendCommand(command);
            return Ok();
        }

        [HttpPost("increasewaterflow")]
        public ActionResult IncreaseWaterFlow(IncreaseWaterFlow command)
        {
            sender.SendCommand(command);
            return Ok();
        }

        [HttpPost("decreasewaterflow")]
        public ActionResult DecreaseWaterFlow(DecreaseWaterFlow command)
        {
            sender.SendCommand(command);
            return Ok();
        }
        [HttpPost("resetcommands")]
        public ActionResult ResetCommands(ResetCommand command)
        {
            command.Reset = true;
            sender.SendCommand(command);
            return Ok();
        }
    }
}
