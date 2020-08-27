namespace SharedModels
{
    public  class DecreaseWaterLevel : ICommand
    {
        public string Name { get; set; }
        public double MinusWaterLevel { get; set; }
        
        public int    StationId { get; set; }
        public virtual void Run()
        {
        }
    }
}