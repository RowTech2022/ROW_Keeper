using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core
{
    public partial class Db
    {
        public partial class User
        {
            [BindStruct]
            public class CheckUser
            {
                [Bind("Login")]
                public string? Login { get; set; }

                [Bind("UserId")]
                public int? UserId { get; set; }

                public CheckUser()
                { }

                public CheckUser(int userId)
                {
                    UserId = userId;
                }

                public CheckUser(string login)
                {
                    Login = login;
                }

                #region c_query

                const string c_query = @"
select
	 u.[Id]
	,u.[BranchId]
	,u.[Login]
	,u.[Phone]
	,u.[PasswordHash]
	,u.[UserType]
	,isnull(o.[Id], 0) as [OrganizationId]
from [new-keeper].[Users] as u
left join [new-keeper].[Organizations] as o on u.[Id] = o.[OwnerId]
where

--{login - start}
	u.Login = @login and
--{login - end}

--{UserId - start}
    u.[Id] = @UserId and
--{UserId - end}

    u.[Active] = 1 and

    1=1
";

                #endregion c_query

                public Result? Exec(ISqlExecutor sql)
                {
                    var query = GetQuery();

                    return sql.Query<Result>(query, this).FirstOrDefault();
                }

                private string GetQuery()
                {
                    var query = c_query;


                    query = SqlQueriesFormater.RemoveSubString(query, UserId == null ? "UserId" : "login");

                    return query;
                }

                [BindStruct]
                public class Result
                {
                    [Bind("Id")]
                    public int UserId { get; set; }
                    
                    [Bind("BranchId")]
                    public int BranchId { get; set; }
                    
                    [Bind("UserType")]
                    public int UserType { get; set; }

                    [Bind("Login")] 
                    public string Login { get; set; } = null!;

                    [Bind("Phone")] 
                    public string Phone { get; set; } = null!;

                    [Bind("PasswordHash", Size = 64)] 
                    public byte[] PasswordHash { get; set; } = null!;
                    
                    [Bind("OrganizationId")] 
                    public int OrganizationId { get; set; }
                }
            }
        }
    }
}
