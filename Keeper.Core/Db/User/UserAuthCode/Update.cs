using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core
{
    public partial class Db
	{
		public partial class UserAuthCode
		{
			[BindStruct]
			public class Update
			{
				[Bind("Id")]
				public int Id { get; set; }

				#region c_query

				const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()
UPDATE
	[new-keeper].[UserAuthCode]
SET 
	[Active] = 0,
	[UpdateAt] = @now
where
     [Id] = @Id or minute([CreatedAt]) >= 5
";
				#endregion c_query

				public void Exec(ISqlExecutor sqlScope)
				{
					sqlScope.Query(c_query, this);
				}
			}
		}
	}
}
