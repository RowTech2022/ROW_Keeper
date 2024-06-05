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
                public class Create
                {
                    [Bind("UserId")]
                    public int UserId { get; set; }
                    
                    [Bind("RegUserId")]
                    public int RegUserId { get; set; }

                    [Bind("RoleId")]
                    public UserRoles RoleId { get; set; }

                    #region c_query

                    const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

insert into [new-keeper].[UserRoleAccess] (
	 [UserId]
    ,[RegUserId]
    ,[RoleId] 
    ,[CreatedAt] )
select
	@UserId,
    @RegUserId,
	@RoleId,
	@now
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
