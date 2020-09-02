using System;
using System.Collections.Generic;
using CommandMicroservice.CommandSender;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

        [HttpGet("getcommands")]
        public ActionResult<List<APIInfo>> GetCommands()
        {
			var toReturn = new List<APIInfo>()
			{
				new APIInfo()
				{
					CommandName="Decrease water level",
					Endpoint="http://commandmicroservice:80/api/command/decreasewaterlevel",
					Rest="Post",
					Gateway="/decreasewaterlevel",
					Command=new DecreaseWaterLevel(){Name = "Decrease water level"}
				},
				new APIInfo()
				{
					CommandName="Increase water level",
					Endpoint="http://commandmicroservice:80/api/command/increasewaterlevel",
					Rest="Post",
					Gateway="/increasewaterlevel",
					Command=new IncreaseWaterLevel(){Name = "Increase water level"}
				},
				new APIInfo()
				{
					CommandName="Increase water flow",
					Endpoint="http://commandmicroservice:80/api/command/increasewaterflow",
					Rest="Post",
					Gateway="/increasewaterflow",
					Command=new IncreaseWaterFlow(){Name = "Increase water flow"}
				},
				new APIInfo()
				{
					CommandName="Decrease water flow",
					Endpoint="http://commandmicroservice:80/api/command/decreasewaterflow",
					Rest="Post",
					Gateway="/decreasewaterflow",
					Command=new DecreaseWaterFlow(){Name = "Decrease water flow"}
				},
				new APIInfo()
				{
					CommandName="Reset water flow and water level",
					Endpoint="http://commandmicroservice:80/api/command/resetcommands",
					Rest="Post",
					Gateway="/reset",
					Command=new ResetCommand(){Name = "Reset"}
				}
			};
			return toReturn;
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
