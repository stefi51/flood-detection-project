using SharedModels;

namespace DeviceMicroservice.DeviceCommand
{
    public class DeviceDecreaseWaterLevel:DecreaseWaterLevel
    {
        private Sensors _sensorsService;

        public DeviceDecreaseWaterLevel(Sensors sensors)
        {
            this._sensorsService = sensors;
        }
        public override void Run()
        {
          //  base.Run();
            System.Diagnostics.Debug.WriteLine(this.MinusWaterLevel.ToString());
        }
    }
}