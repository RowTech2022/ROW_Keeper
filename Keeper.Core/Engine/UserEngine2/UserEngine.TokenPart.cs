using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core
{
    public partial class UserEngine2
	{
		public UserIdentityFull GetUserInfoForToken(string username, string password)
		{
			var getDb = new Db.User.CheckUser(username).Exec(m_sql);
			if (getDb == null)
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserNotFound"));

			var hashtoCheck = m_hashCalculator.GetPasswordHash(getDb.UserId, password);

			return GetUserInfoForToken(getDb, hashtoCheck);
		}

		public UserIdentityFull GetUserInfoForRefreshToken(int userId, byte[] hashtoCheck)
		{
			var getDb = new Db.User.CheckUser() { UserId = userId }.Exec(m_sql);
			if (getDb == null)
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserNotFound"));

			return GetUserInfoForToken(getDb, hashtoCheck);
		}

		UserIdentityFull GetUserInfoForToken(Db.User.CheckUser.Result getDb, byte[] hashtoCheck)
		{
			if ((!m_ignorePassword) && !HashHelper.EqualArrays(getDb.PasswordHash, hashtoCheck))
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserOrPasswordIncorrect"));

			var db = GetDbUser(getDb.UserId);

			var result = new UserIdentityFull(db.Id, db.Login, "db.OrgName");
			result.PasswordHash = getDb.PasswordHash;

			result.Roles = GetUserRoles(db.Id);

			return result;
		}

		public UserInfoExtension AuthCheckUser(string username, string password)
		{
			var getDb = new Db.User.CheckUser(username).Exec(m_sql);
			if (getDb == null)
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserNotFound"));

			var hashtoCheck = m_hashCalculator.GetPasswordHash(getDb.UserId, password);

			if ((!m_ignorePassword) && !HashHelper.EqualArrays(getDb.PasswordHash, hashtoCheck))
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserOrPasswordIncorrect"));

			var db = GetDbUser(getDb.UserId);

			var result = new UserInfoExtension();
			result.PasswordHash = getDb.PasswordHash;
			result.UserId = db.Id;
			result.OrganisationId = 0;

			result.Roles = GetUserRoles(db.Id).ToHashSet();

			return result;
		}

		public UserInfoExtension RefreshCheckUser(int userId, byte[] hashtoCheck)
		{
			var getDb = new Db.User.CheckUser() { UserId = userId }.Exec(m_sql);
			if (getDb == null)
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserNotFound"));

			if ((!m_ignorePassword) && !HashHelper.EqualArrays(getDb.PasswordHash, hashtoCheck))
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserOrPasswordIncorrect"));

			var db = GetDbUser(getDb.UserId);

			var result = new UserInfoExtension();
			result.PasswordHash = getDb.PasswordHash;
			result.UserId = db.Id;
			result.OrganisationId = 0;

			result.Roles = GetUserRoles(db.Id).ToHashSet();

			return result;
		}

	}
}