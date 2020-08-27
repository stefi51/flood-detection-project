namespace SharedModels
{
    public interface  ICommand
    {
        public string Name { get; set; }

        public abstract void Run();

    }
}