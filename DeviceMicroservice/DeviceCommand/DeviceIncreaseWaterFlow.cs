using SharedModels;

namespace DeviceMicroservice.DeviceCommand
{
    public class DeviceIncreaseWaterFlow:IncreaseWaterFlow
    {
        private Sensors _sensorsService;

        public DeviceIncreaseWaterFlow(Sensors sensors)
        {
            this._sensorsService = sensors;
        }
        public override void Run()
        {
            // base.Run();
            //System.Diagnostics.Debug.WriteLine(this.PlusWaterLevel.ToString());
        }
    }
}