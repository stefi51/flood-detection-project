namespace Gateway.RouterModel
{
	public class Destination
	{
		public string Path { get; set; }
		public Destination(string uri) { Path = uri; }
		private Destination() { Path = "/"; }
	}
}
