using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
	public partial class UserAuthCode
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
				[Bind("Id")]
				public int Id { get; set; }

				[Bind("Code")]
				public int? Code { get; set; }
			}

			#region c_query

			const string c_query = @"
select
	 u.[Id]
	,u.[Code]
FROM [new-keeper].[UserAuthCode] as u
WHERE
    --{UserIds - start}
    u.UserId in ({UserIds}) and
    --{UserIds - end}
    
    minute(u.[CreatedAt]) <= 5 and

    u.Active = 1

ORDER BY Id DESC
";

			#endregion c_query

			public List<Result> Exec(ISqlExecutor sql)
			{
				var query = GetQuery();

				return sql.Query<Result>(query, this).ToList();
			}

			private string GetQuery()
			{
				var query = c_query;

				query = SqlQueriesFormater.RemoveOrReplace("UserIds", UserIds, x => string.Join(",", x)).Format(query);

				return query;
			}
		}
	}
}