using Row.Common1;
using Row.Common1.Client1;
using Row.Common1.Dto1;

namespace Keeper.Core
{
    public partial class UserEngine
	{
		public UserIdentityFull GetUserInfoForToken(string username, string password)
		{
			var getDb = new Db.User.CheckUser(username).Exec(m_sql);
			if (getDb == null)
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserNotFound"));

			var hashToCheck = m_hashCalculator.GetPasswordHash(getDb.UserId, password);

			return GetUserInfoForToken(getDb, hashToCheck);
		}

		public UserIdentityFull GetUserInfoForRefreshToken(int userId, byte[] hashtoCheck)
		{
			var getDb = new Db.User.CheckUser { UserId = userId }.Exec(m_sql);
			if (getDb == null)
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserNotFound"));

			return GetUserInfoForToken(getDb, hashtoCheck);
		}

		UserIdentityFull GetUserInfoForToken(Db.User.CheckUser.Result getDb, byte[] hashtoCheck)
		{
			if ((!m_ignorePassword) && !HashHelper.EqualArrays(getDb.PasswordHash, hashtoCheck))
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserOrPasswordIncorrect"));

			var db = GetDbUser(getDb.UserId);

			var result = new UserIdentityFull(db.Id, db.Login, "db.OrgName")
			{
				PasswordHash = getDb.PasswordHash,
				Roles = GetUserRoles(db.Id)
			};

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

			var result = new UserInfoExtension
			{
				PasswordHash = getDb.PasswordHash,
				UserId = getDb.UserId,
				Expired = DateTime.Now.AddHours(1),
				SessionId = Guid.NewGuid(),
				Type = getDb.UserType,
				Roles = GetUserRoles(getDb.UserId).ToHashSet(),
				OrganisationId = getDb.OrganizationId,
			};

			return result;
		}

        public Db.User.CheckUser.Result SendCodeCheckUser(string username, string password)
        {
            var getDb = new Db.User.CheckUser(username).Exec(m_sql);
            if (getDb == null)
                throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserNotFound"));

            var hashtoCheck = m_hashCalculator.GetPasswordHash(getDb.UserId, password);

            if ((!m_ignorePassword) && !HashHelper.EqualArrays(getDb.PasswordHash, hashtoCheck))
                throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserOrPasswordIncorrect"));

			return getDb;
		}

        public UserInfoExtension RefreshCheckUser(int userId, byte[] hashtoCheck)
		{
			var getDb = new Db.User.CheckUser { UserId = userId }.Exec(m_sql);
			if (getDb == null)
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserNotFound"));

			if ((!m_ignorePassword) && !HashHelper.EqualArrays(getDb.PasswordHash, hashtoCheck))
				throw new InCorrectUserOrPasswordApiException(m_languageServices.GetKey("UserOrPasswordIncorrect"));

			var db = GetDbUser(getDb.UserId);

			var result = new UserInfoExtension
			{
				PasswordHash = getDb.PasswordHash,
				UserId = db.Id,
				OrganisationId = 0,
				Roles = GetUserRoles(db.Id).ToHashSet()
			};

			return result;
		}

        public List<UserRoles> GetUserRoles(int userId)
        {
            var roles = new Db.User.UserRoleAccess.List(userId).Exec(m_sql);
            if (roles.Count == 0)
                throw new Exception($"{m_languageServices.GetKey("UserWithEmptyRols")} UserId = {userId}");

            return roles.Select(x => x.RoleId).ToList();
        }

    }
}