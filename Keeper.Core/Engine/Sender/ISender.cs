namespace Keeper.Core
{
    public interface ISender
	{
		Task SendRequest(MailRequest request, bool cooruption);
	}

	public class MailRequest
	{
		public string[] ByParams { get; set; }
		public string Body { get; set; }
		public List<string> Files { get; set; } = new List<string>();
	}
}
