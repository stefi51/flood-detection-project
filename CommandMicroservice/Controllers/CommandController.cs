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

        [HttpGet("")]
        public JsonResult GetCommands()
        {
            return new JsonResult(
                new
                {
                    Name= "Commands",
                    CommandList=new List<object>()
                    {
                        new
                        {
                            commandName="Decrease water level",
                            endpoint="",
                            rest="Post",
                            command=new DecreaseWaterLevel(){Name = "Decrease water level"}
                        },
                        new
                        {
                            commandName="Increase water level",
                            endpoint="",
                            rest="Post",
                            command=new IncreaseWaterLevel(){Name = "Increase water level"}
                        },
                        new
                        {
                            commandName="Increase water flow",
                            endpoint="",
                            rest="Post",
                            command=new IncreaseWaterFlow(){Name = "Increase water flow"}

                        },
                        new
                        {
                            commandName="Decrease water flow",
                            endpoint="",
                            rest="Post",
                            command=new DecreaseWaterFlow(){Name = "Decrease water flow"}
                        },
                        new
                        {
                            commandName="Reset water flow and water level",
                            endpoint="",
                            rest="Post",
                            command=new ResetCommand(){Name = "Reset"}
                        },

                    }
                });
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
