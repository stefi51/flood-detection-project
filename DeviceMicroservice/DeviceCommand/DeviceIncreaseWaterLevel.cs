using DeviceMicroservice.Repositories;
using SharedModels;

namespace DeviceMicroservice.DeviceCommand
{
    public class DeviceIncreaseWaterLevel:IncreaseWaterLevel
    {
        private IDataRepository dataRepository;

        public DeviceIncreaseWaterLevel(IDataRepository repository)
        {
            this.dataRepository = repository;
        }
        public override void Run()
        {
           // base.Run();
          //  System.Diagnostics.Debug.WriteLine(this.PlusWaterLevel.ToString());
          dataRepository.UpdateStationWaterLevel(this.StationId,this.PlusWaterLevel);
        }
    }
}