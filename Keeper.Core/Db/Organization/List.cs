using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Organization
    {
        public class List(params int[] ids)
        {
            public int[] Ids { get; set; } = ids;

            public class Result : BaseProp
            {
                [Bind("Id")]
                public int Id { get; set; }
                
                [Bind("OwnerId")]
                public int OwnerId { get; set; }
                
                [NVarChar("OrgName", 500)]
                public string OrgName { get; set; } = null!;
                
                [NVarChar("OrgPhone", 20)]
                public string OrgPhone { get; set; } = null!;
                
                [NVarChar("OrgEmail", 20)]
                public string? OrgEmail { get; set; }
                
                [NVarChar("OrgAddress", 500)]
                public string OrgAddress { get; set; } = null!;
            }

            #region c_query

            private const string Query = @"
select 
     o.[Id]
    ,o.[OwnerId]
    ,o.[OrgName]
    ,o.[OrgPhone]
    ,o.[OrgEmail]
    ,o.[OrgAddress]
    ,o.[Active]
    ,o.[CreatedAt]
    ,o.[UpdatedAt]
    ,o.[Timestamp]
from [new-keeper].[Organizations] as o
where
    
    --{Ids - start}
    o.[Id] in ({Ids}) and
    --{Ids - end}
    
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
                var query = Query;

                query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(", ", x))
                    .Format(query);

                return query;
            }
        }
    }
}