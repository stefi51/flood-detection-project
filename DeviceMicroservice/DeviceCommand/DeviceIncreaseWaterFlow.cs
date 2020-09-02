using DeviceMicroservice.Repositories;
using SharedModels;

namespace DeviceMicroservice.DeviceCommand
{
    public class DeviceIncreaseWaterFlow:IncreaseWaterFlow
    {
        private IDataRepository dataRepository;

        public DeviceIncreaseWaterFlow(IDataRepository repository)
        {
            this.dataRepository = repository;
        }
        public override void Run()
        {
            dataRepository.UpdateStationWaterFlow(this.StationId, this.PlusWaterFlow);
        }
    }
}