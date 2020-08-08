using SharedModels;

namespace DeviceMicroservice.DeviceCommand
{
    public class DeviceReduceWaterLevel:ReduceWaterLevel
    {
        private Sensors _sensorsService;

        public DeviceReduceWaterLevel(Sensors sensors)
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