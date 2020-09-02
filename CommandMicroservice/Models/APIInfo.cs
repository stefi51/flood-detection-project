using SharedModels;

namespace CommandMicroservice
{
	public class APIInfo
	{
		public string CommandName { get; set; }
		public string Endpoint { get; set; }
		public string Rest { get; set; }
		public ICommand Command { get; set; }
		public string Gateway { get; set; }
	}
}