using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core
{
    public partial class Db
    {
        public partial class UserAuthCode
        {
            [BindStruct]
            public class Create
            {
				[Bind("UserId")]
				public int UserId  { get; set;}

				[Bind("Code")]
				public int? Code { get; set; }

				[NVarChar("PhoneNumber", 20)]
				public string? PhoneNumber  { get; set;} 

				[Bind("Active")]
				public bool Active  { get; set;} 

				#region c_query

				const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

INSERT INTO [new-keeper].[UserAuthCode]
    (
     [UserId]
    ,[Code]
    ,[PhoneNumber]
    ,[Active]
    ,[CreatedAt]
    ,[UpdatedAt]
    )
select
    @UserId,
    @Code,
    @PhoneNumber,
    @Active,
    @now,
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
