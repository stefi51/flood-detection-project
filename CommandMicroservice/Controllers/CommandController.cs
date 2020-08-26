using System;
using CommandMicroservice.CommandSender;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

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
            sender.SendCommand(new ReduceWaterLevel(){Name = "Reduce Water Level",MinusWaterLevel = 10.0});
            return Ok();
        }
    }
}
