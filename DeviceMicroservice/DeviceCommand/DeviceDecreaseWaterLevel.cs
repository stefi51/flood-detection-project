using DeviceMicroservice.Repositories;
using SharedModels;

namespace DeviceMicroservice.DeviceCommand
{
    public class DeviceDecreaseWaterLevel:DecreaseWaterLevel
    {
        private IDataRepository dataRepository;

        public DeviceDecreaseWaterLevel(IDataRepository repository)
        {
            this.dataRepository = repository;
        }
        public override void Run()
        {
            dataRepository.UpdateStationWaterLevel(this.StationId,-this.MinusWaterLevel);
        }
    }
}