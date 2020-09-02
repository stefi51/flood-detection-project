using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels.Commands
{
    public class ResetCommand:ICommand
    {
        public string Name { get; set; }
        public  int StationId { get; set; }
        public  bool Reset { get; set; }
        public virtual void Run()
        { }
    }
}
