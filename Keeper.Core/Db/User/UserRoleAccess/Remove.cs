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
                public class Remove
                {
                    [Bind("UserId")]
                    public int UserId { get; set; }

                    [Bind("RoleId")]
                    public UserRoles RoleId { get; set; }

                    #region c_query

                    const string c_query = @"
                        delete from [new-keeper].[UserRoleAccess] where [UserId] = @UserId and [RoleId] = @RoleId
                    ";

                    #endregion c_query

                    public void Exec(ISqlExecutor scopeSql)
                    {
                        scopeSql.Query(c_query, this);
                    }
                }
            }
        }
    }
}
