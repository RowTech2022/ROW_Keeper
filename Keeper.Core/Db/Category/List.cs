using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Category
    {
        [BindStruct]
        public class List(params int[] ids)
        {
            public int[]? Ids { get; set; } = ids;
            
            [BindStruct]
            public class Result : BaseProp
            {
                [Bind("Id")]
                public int Id { get; set; }
                
                [Bind("OrgId")]
                public int OrgId { get; set; }
                
                [Bind("ParentId")]
                public int? ParentId { get; set; }
    
                [NVarChar("Name", 300)]
                public string Name { get; set; } = null!;
            }

            #region c_query

            private const string c_query = @"
select 
     c.[Id]
    ,c.[OrgId]
    ,c.[ParentId]
    ,c.[Name]
    ,c.[CreatedAt]
    ,c.[UpdatedAt]
    ,c.[Timestamp]
from [new-keeper].[Categories] as c
where
    
    --{Ids - start}
    c.[Id] in ({Ids}) and
    --{Ids - end}
    
    c.[Active] = 1 and
    
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

                return query;
            }
        }
    }
}