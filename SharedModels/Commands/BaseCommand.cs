namespace SharedModels
{
    public class BaseCommand : ICommand
    {
        public string Name { get; set; }
        public virtual void Run()
        { }
    }
}