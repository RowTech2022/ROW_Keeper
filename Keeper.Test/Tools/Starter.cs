using Bibliotekaen.Sql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Reflection;
using Keeper.Core;
using Keeper.Client;

namespace Keeper.Test
{
    public class SendTestStr : ISendStrategy
	{
		private readonly Dictionary<string, int> _codes = new();
		private readonly Dictionary<string, string> _passwords = new();

		public int GetLastCode(string userName)
		{
			return _codes.TryGetValue(userName, out var code) ? code : 0;
		}

		public string GetLastPassword(string login)
		{
			return _passwords.TryGetValue(login, out var value) ? value : "";
		}

		public void SendCodeToPhone(User.UserCode request, string phone, string code, string userName)
		{
			_codes[request.Login] = int.Parse(code);
		}


		public void SendLoginAndPasswordToPhone(string phone, string login, string password)
		{
			_passwords[login] = password;
		}
	}

	public class TestSettings
	{
		public SendTestStr SendSmsMock { get; set; } = new();
	}


	class Starter : WebApplicationFactory<Program>, IDisposable
	{
		public bool IgnoreCode { get; set; }

		public string BaseUrl = "http://localhost:8526";

		public ISqlFactory Sql { get; set; }

		public HttpClient ClientHttp { get; set; }
		public KeeperApiClient Client { get; set; }

		public TestSettings Settings { get; set; } = new TestSettings();

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				var dbContextDescriptor = services.SingleOrDefault(
					d => d.ServiceType == typeof(AuthEngine));
				if (dbContextDescriptor != null)
				{
					var engine = (AuthEngine)dbContextDescriptor.ImplementationInstance!;
					engine.SetSmsStrategy(Settings.SendSmsMock);
				}

				var userEngine = services.SingleOrDefault(
					d => d.ServiceType == typeof(UserEngine2));
				if (userEngine != null)
				{
					var engine = (UserEngine2)userEngine.ImplementationInstance!;
					engine.IgnoreCode = IgnoreCode;
				}
				
				var userEnginePrev = services.SingleOrDefault(
					d => d.ServiceType == typeof(UserEngine));
				if (userEnginePrev != null)
				{
					var engine = (UserEngine)userEnginePrev.ImplementationInstance!;
					engine.AuthEngine.SetSmsStrategy(Settings.SendSmsMock);
				}
			});
			base.ConfigureWebHost(builder);
		}

		public Starter(bool ignoreCode = true)
		{
			IgnoreCode = ignoreCode;

			ClientOptions.BaseAddress = new Uri("http://localhost:8526");

			ClientHttp = CreateClient();

			Sql = new SqlFactory("Server=tcp:row.database.windows.net;Database=row-db;Persist Security Info=True;User ID=rowadmin;Password=bkSpA2rAxTdKGct;MultipleActiveResultSets=True;");

			Client = new KeeperApiClient(BaseUrl);
			Client.InitHttp(ClientHttp);
		}

		public Starter TestLogin()
		{
			Login(Context.TestLogin, Context.TestPassword);
			return this;
		}

		public void Login(string username, string password)
		{
			var login = new Login();
			login.UserLogin = username;
			login.Password = password;

			if (!IgnoreCode)
			{
				var getCode = new User.UserCode()
				{
					Login = username,
					// Password = "123"
				};
				getCode.ExecTest(Client);

				var code = Settings.SendSmsMock.GetLastCode(username);
				//login.Code = code;
			}

			var token = login.ExecTest(Client);

			ClientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
		}

		public string LoadFile(string path, string fileName)
		{
			var currentDirectorey = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var ind = currentDirectorey.IndexOf("\\Keeper.Test");
			var path1 = currentDirectorey.Substring(0, ind);
			var fileForCopy = Path.Combine(path1, path);

			var prevPath = $"{new Random().Next(1, 999)}_{DateTime.Now.ToString("FFFFFF")}";
			var saveTo = Path.Combine("C:\\home\\public", Helper.FileTempName.Substring(1), prevPath);//, fileName);
			if (!Directory.Exists(saveTo))
				Directory.CreateDirectory(saveTo);
			var saveTo1 = Path.Combine("C:\\home\\public", Helper.FileTempName.Substring(1), prevPath, fileName);//, fileName);

			File.Copy(fileForCopy, saveTo1, true);

			var tempPath = Path.Combine(Helper.FilePublicPath, Helper.FileTempName.Substring(1), prevPath, fileName);

			return FileEngine.GetFileWithHashWithFileName(tempPath);
		}

		public void Dispose()
		{
			ClientHttp.Dispose();
		}
	}
}
