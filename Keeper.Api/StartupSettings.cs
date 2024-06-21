using Bibliotekaen.Sql;
using Newtonsoft.Json;
using Row.Common1;
using Row.Common1.Client1;
using System.Globalization;
using Keeper.Core;
using Keeper.Client;
using static System.Boolean;

namespace Keeper.Api
{
	public class StartupSettings
	{
		public static Language DefaultLanguage1 = Language.TJ;

		public AllCommonSettings Common { get; set; } = null!;

		public ISqlFactory Sql { get; set; } = null!;
		public ISendStrategy SMSSend { get; set; } = null!;


		public bool IgnorePassword { get; set; }
		public bool IgnoreCode { get; set; }

		public string? AuthMessText { get; set; }
		public string? MessUrl { get; set; }

		JsonSerializer? JsonSerializer { get; set; }

		public string FileStoragePath { get; set; } = "";

		public AuthServerSettings AuthServerSettings { get; set; } = new AuthServerSettings();

		public EmailSender.SenderSetting SenderSettings { get; set; } = null!;

		public StartupSettings Load(List<IConfigurationSection> allValues)
		{
			Common = new AllCommonSettings().LoadCommonSettings(allValues);

			JsonSerializer = CreateSerializer();
			AuthServerSettings.Serializer = JsonSerializer;
			var expireTime = allValues.First(x => x.Key == "Auth.TokenExpiredTime").Value;
			if (string.IsNullOrWhiteSpace(expireTime))
				throw new Exception("Expire time cannot be null or empty");
			
			AuthServerSettings.ExpireTime = TimeSpan.Parse(expireTime);

			var refreshExpireTime = allValues.First(x => x.Key == "Auth.RefreshExpireTime").Value;
			if (string.IsNullOrWhiteSpace(refreshExpireTime))
				throw new Exception("Refresh expire time cannot be null or empty");
			
			AuthServerSettings.RefreshExpireTime = TimeSpan.Parse(refreshExpireTime);
			
			var fileStorage = allValues.First(x => x.Key == "File.StoragePath").Value;
			if (string.IsNullOrWhiteSpace(fileStorage))
				throw new Exception("File storage cannot be null or empty.");
			
			FileStoragePath = fileStorage;

			var sqlCon = allValues.FirstOrDefault(x => x.Key == "SqlData")?.Value;
			if (String.IsNullOrEmpty(sqlCon))
				throw new ValidationApiException("Не правильно указано строка подключение к БД");
			Sql = new SqlFactory(sqlCon);

			var authMessText = allValues.FirstOrDefault(x => x.Key == "AuthMessText")?.Value;
			if (String.IsNullOrEmpty(authMessText))
				throw new ValidationApiException("Не указан текст для авторизаций");

			var messUrl = allValues.FirstOrDefault(x => x.Key == "MessUrl")?.Value;
			if (String.IsNullOrWhiteSpace(messUrl))
				throw new ValidationApiException("Смс адресс не можеть быт пустым.");

			SMSSend = new SmsStrategy(messUrl, authMessText);

			TryParse(allValues
				.FirstOrDefault(config => config.Key == "IgnorePassword")?.Value, out bool ignorePassword);
			IgnorePassword = ignorePassword;

			TryParse(allValues
				.FirstOrDefault(config => config.Key == "IgnoreCode")?.Value, out bool ignoreCode);
			IgnoreCode = ignoreCode;

			AuthServerSettings.IgnorePassword = IgnorePassword;
			AuthServerSettings.IgnoreCode = IgnoreCode;
			AuthServerSettings.DebugMode = Common.Debug.Enabled;

			SenderSettings = new EmailSender.SenderSetting().Load(allValues);

			return this;
		}

		private JsonSerializer CreateSerializer()
		{
			var jsonSerializer = new JsonSerializer
			{
				NullValueHandling = NullValueHandling.Ignore,
				Culture = CultureInfo.InvariantCulture,
				Formatting = Formatting.None,
				DateTimeZoneHandling = DateTimeZoneHandling.Utc
			};
			return jsonSerializer;
		}
	}

	public static class HelperApi
	{
		public static Language GetCultureInfo(this HttpContext context)
		{
			var ln = context.Request.Headers.AcceptLanguage;

			switch (ln)
			{
				case "en-Us":
				case "en":
				case "EN":
					return Language.EN;
				case "ru-Ru":
				case "ru":
				case "RU":
					return Language.RU;
				default:
					return Language.TJ;
			}
		}
	}
}
