using DeviceMicroservice.Repositories;
using SharedModels;

namespace DeviceMicroservice.DeviceCommand
{
    public class DeviceDecreaseWaterFlow:DecreaseWaterFlow
    {
        private IDataRepository dataRepository;

        public DeviceDecreaseWaterFlow(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }
        public override void Run()
        {
            dataRepository.UpdateStationWaterFlow(this.StationId,-this.MinusWaterFlow);
        }
        
    }
}