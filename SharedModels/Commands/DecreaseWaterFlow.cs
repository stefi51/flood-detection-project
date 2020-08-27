namespace SharedModels
{
    public class DecreaseWaterFlow:ICommand
    {
        public string Name { get; set; }
        public double MinusWaterFlow { get; set; }
        public int    StationId { get; set; }
        public virtual void Run()
        {
            
        }
    }
}