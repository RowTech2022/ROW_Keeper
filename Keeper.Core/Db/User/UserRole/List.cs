using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;
using Row.Common1.Dto1;

namespace Keeper.Core
{
    public partial class Db
    {
        public partial class User
        {
            public partial class UserRoleAccess
            {
                [BindStruct]
                public class List
                {
                    public int[] UserIds { get; set; }

                    public List(params int[] ids)
                    {
                        UserIds = ids;
                    }

                    [BindStruct]
                    public class Result
                    {

                        [Bind("UserId")]
                        public int UserId { get; set; }

                        [Bind("RoleId")]
                        public UserRoles RoleId { get; set; }

                        [NVarChar("RoleName", 100)] 
                        public string RoleName { get; set; } = null!;
                    }

                    #region c_query

                    const string c_query = @"
select 
     u.[UserId]
    ,u.[RoleId]
    ,r.[RoleName]
from [new-keeper].UserRoleAccess u
inner join  [keeper].UserRole r 
on u.RoleId = r.RoleId
where 	

--{UserIds - start}
	u.UserId in ({UserIds}) and
--{UserIds - end}

1=1
";

                    #endregion c_query

                    public List<Result> Exec(ISqlExecutor sql)
                    {
                        var query = GetQuery();

                        return sql.Query<Result>(query, this).ToList();
                    }

                    public string GetQuery()
                    {
                        var query = c_query;

                        if (UserIds.Length == 0)
                            query = SqlQueriesFormater.RemoveSubString(query, "UserIds");

                        query = SqlQueriesFormater.RemoveOrReplace("UserIds", UserIds, x => string.Join(",", x)).Format(query);


                        return query;
                    }

                }
            }
        }
    }
}