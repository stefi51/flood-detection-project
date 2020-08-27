namespace SharedModels
{
    public class IncreaseWaterLevel : ICommand
    {
        public string Name { get; set; }
        public double PlusWaterLevel { get; set; }
        
        public  int StationId { get; set; }
        public virtual void Run()
        {
        }
    }

}