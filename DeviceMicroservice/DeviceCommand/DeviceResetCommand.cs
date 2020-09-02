using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceMicroservice.Repositories;
using SharedModels.Commands;

namespace DeviceMicroservice.DeviceCommand
{
    public class DeviceResetCommand:ResetCommand
    {
        private IDataRepository dataRepository;
        public DeviceResetCommand(IDataRepository repository)
        {
            dataRepository = repository;
        }
        public override void Run()
        {
            dataRepository.UpdateStationParameter(this.StationId, 0.0,0.0);
        }
    }
}
