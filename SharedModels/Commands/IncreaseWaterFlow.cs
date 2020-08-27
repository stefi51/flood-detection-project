namespace SharedModels
{
    public class IncreaseWaterFlow:ICommand
    {
        public string Name { get; set; }
        public double PlusWaterFlow { get; set; }
        public  int StationId { get; set; }
        public virtual void Run()
        {
            
        }
    }
}