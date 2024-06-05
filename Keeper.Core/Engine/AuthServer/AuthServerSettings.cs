using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace Keeper.Core
{
    public class AuthServerSettings
	{
		public TimeSpan ExpireTime { get; set; }
		public TimeSpan RefreshExpireTime { get; set; }
		public IDataProtectionProvider? TokenProtector { get; set; }
		public JsonSerializer? Serializer { get; set; }
		public IDataProtector? AuthProtector { get; set; }

		public bool IgnorePassword { get; set; }
		public bool IgnoreCode { get; set; }
		public bool DebugMode { get; set; }

	}
}
