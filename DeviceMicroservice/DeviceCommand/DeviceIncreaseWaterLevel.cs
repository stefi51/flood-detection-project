using SharedModels;

namespace DeviceMicroservice.DeviceCommand
{
    public class DeviceIncreaseWaterLevel:IncreaseWaterLevel
    {
        private Sensors _sensorsService;

        public DeviceIncreaseWaterLevel(Sensors sensors)
        {
            this._sensorsService = sensors;
        }
        public override void Run()
        {
           // base.Run();
            System.Diagnostics.Debug.WriteLine(this.PlusWaterLevel.ToString());
        }
    }
}