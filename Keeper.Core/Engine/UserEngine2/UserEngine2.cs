using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Row.Common1.Dto1;

namespace Keeper.Core
{
    public partial class UserEngine2
	{
		ISqlFactory m_sql;
		DtoComplex m_dto;
		HashCalculator m_hashCalculator = new HashCalculator("#####");
		FileEngine m_fileEngine;

		bool m_ignorePassword;
		public bool IgnoreCode { get; set; }

		LanguageService m_languageServices;

		public UserEngine2(ISqlFactory sql, DtoComplex dto, FileEngine fileApi, bool ignorePassword, bool ignoreCode)
		{
			m_sql = sql;
			m_dto = dto;
			m_fileEngine = fileApi;
			m_ignorePassword = ignorePassword;

			IgnoreCode = ignoreCode;
		}

		public UserEngine2 InitLanguageServise(LanguageService languageService)
		{
			m_languageServices = languageService;
			return this;
		}

        public List<UserRoles> GetUserRoles(int userId)
        {
            var roles = new Db.User.UserRoleAccess.List(userId).Exec(m_sql);
            if (!roles.Any())
                throw new Exception($"{m_languageServices.GetKey("UserWithEmptyRols")} UserId = {userId}");

            return roles.Select(x => x.RoleId)?.ToList() ?? new List<UserRoles>();
        }

        public Db.User.List.Result GetDbUser(int userId)
		{
			var user = new Db.User.List(userId).Exec(m_sql).FirstOrDefault();
			if (user == null)
				throw new Exception(m_languageServices.GetKey("RecordNotFound"));

			return user;
		}



	}
}