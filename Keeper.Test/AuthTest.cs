using Bibliotekaen.Sql;
using FluentAssertions;
using Row.Common1.Client1;
using System.Net.Http.Headers;
using Keeper.Client;

namespace Keeper.Test
{
    [TestClass]
	public class AuthTest
	{
		List<int> CreatedUsers = new List<int>();
		ISqlFactory m_sql;

		[TestInitialize]
		public void Init()
		{
			//begin transaction
		}

		[TestCleanup]
		public void Cleanup()
		{
			if (m_sql != null)
			{
				if (CreatedUsers.Any())
				{
					var userRoleDelete = $"delete from [row].[UserRoleAccess] WHERE UserId in ({string.Join(",", CreatedUsers)})";
					m_sql.Query(userRoleDelete);
					Console.WriteLine(userRoleDelete);
					var userDelete = $"delete from [row].[User] WHERE Id in ({string.Join(",", CreatedUsers)})";
					m_sql.Query(userDelete);
					Console.WriteLine(userDelete);
				}
			}
		}

		public void InitCleaner(ISqlFactory sql)
		{
			m_sql = sql;
		}

		public void AddUserForCleaner(int id)
		{
			CreatedUsers.Add(id);
		}

		[TestMethod]
		public void Login_Refresh_test()
		{
			using (var server = new Starter().TestLogin())
			{
				InitCleaner(server.Sql);

				var create = Context.DefaultUser();
				//create.Password = "123456";
				//create.ConfirmPassWord = "123456";
				var result = create.ExecTest(server.Client);

				var login = new Login();
				login.UserLogin = "dfd";
                login.Password = "123456";
				// login.GrantType = "password";

				var token = login.ExecTest(server.Client);
				token.AccessToken.Should().NotBeNull();
				token.RefreshToken.Should().NotBeNull();

				var refresh = new Refresh();
				refresh.RefreshToken = token.RefreshToken;

				token = refresh.ExecTest(server.Client);
				token.AccessToken.Should().NotBeNull();
				token.RefreshToken.Should().NotBeNull();

				server.ClientHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

				//var details = UserDetails.ExecTest(server.Client);

				//details.UserId.Should().Be(result.Id);

				//change the password
				var updatePassword = new User.UpdatePassword();
				updatePassword.OldPassWord = "123456";
				updatePassword.NewPassWord = "111111";

				updatePassword.ExecTest(server.Client);

				AssertionExtensions.Should(() => refresh.ExecTest(server.Client))
				.Throw<InCorrectUserOrPasswordApiException>();
				//.WithMessage("Логин или парол неверны");

				AddUserForCleaner(result.Id);
			}
		}

		[TestMethod]
		public void CheckDfoAuthTest()
		{
			using (var server = new Starter(false).TestLogin())
			{
				var create = Context.DefaultUser();
				var result = create.ExecTest(server.Client);

				var getCode = new User.UserCode()
				{
					Login = result.Login,
					// Password = "123",
				};

				getCode.ExecTest(server.Client);

				// var code = server.Settings.SendSmsMock.GetLastCode(create.Inn);

				var requestUser = new Login()
				{
					// UserLogin = create.Inn,
					Password = "123",
					// GrantType = "password",
				};

				var token = requestUser.ExecTest(server.Client);
				token.AccessToken.Should().NotBeNull();
				token.RefreshToken.Should().NotBeNull();

				AddUserForCleaner(result.Id);
			}
		}
	}
}