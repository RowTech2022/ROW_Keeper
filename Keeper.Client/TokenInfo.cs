namespace Keeper.Client
{
    public class TokenInfo
    {
        public string? AccessToken { get; set; }
        public string? TokenType { get; set; }
        public int? ExpireTime { get; set; }
        public string? RefreshToken { get; set; }
	}

	public interface ITest
	{
		string TestField { get; set; }
	}
}