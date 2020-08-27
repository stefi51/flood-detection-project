using SharedModels;

namespace DeviceMicroservice.DeviceCommand
{
    public class DeviceDecreaseWaterFlow:DecreaseWaterFlow
    {
        private Sensors _sensorsService;

        public DeviceDecreaseWaterFlow(Sensors sensors)
        {
            this._sensorsService = sensors;
        }
        public override void Run()
        {
            //  base.Run();
           // System.Diagnostics.Debug.WriteLine(this.MinusWaterLevel.ToString());
        }
        
    }
}