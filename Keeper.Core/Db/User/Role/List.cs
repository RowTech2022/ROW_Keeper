using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class User
    {
        public partial class Role
        {
            public class List(params int[] ids)
            {
                public int[] Ids { get; set; } = ids;
                
                [BindStruct]
                public class Result
                {
                    [Bind("RoleId")]
                    public int RoleId { get; set; }
                    
                    [NVarChar("RoleName", 200)]
                    public string RoleName { get; set; } = null!;
                }

                #region c_query

                private string c_query = @"
select
     u.[RoleId]
    ,u.[RoleName]
from [new-keeper].[UserRole] as u
where
    
    u.[RoleId] != 100 and
    1 = 1
";

                #endregion

                public List<Result> Exec(ISqlExecutor sql)
                {
                    var query = GetQuery();

                    return sql.Query<Result>(query).ToList();
                }

                private string GetQuery()
                {
                    var query = c_query;

                    query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(", ", x)).Format(query);

                    return SqlQueriesFormater.RemoveLabels(query);
                }
            }
        }
    }
}