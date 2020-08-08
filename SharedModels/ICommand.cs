namespace SharedModels
{
    public interface  ICommand
    {
        public string Name { get; set; }

        public abstract void Run();

    }

    public class BaseCommand : ICommand
    {
        public string Name { get; set; }
        public virtual void Run()
        { }
    }
    
    public  class ReduceWaterLevel : ICommand
    {
        public string Name { get; set; }
        public double MinusWaterLevel { get; set; }
        public virtual void Run()
        {
            
        }

    }

    public class IncreaseWaterLevel : ICommand
    {
        public string Name { get; set; }
        public double PlusWaterLevel { get; set; }
        public virtual void Run()
        {
            
        }
    }


}