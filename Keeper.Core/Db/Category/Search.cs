using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Category
    {
        public class Search
        {
            public int[]? Ids { get; set; }

            [NVarChar("Name", 300)]
            public string? Name { get; set; }

            public int? Count { get; set; }
            public int? Start { get; set; }
            
            [BindStruct]
            public class Result
            {
                [Bind("Id")]
                public int Id { get; set; }
                
                [Bind("ParentId")]
                public int ParentId { get; set; }
    
                [NVarChar("Name", 300)]
                public string Name { get; set; } = null!;
    
                [NVarChar("Description", 300)]
                public string? Description { get; set; }

                [Bind("Total")]
                public int Total { get; set; }
            }

            #region c_query

            private const string c_query = @"
select
{topPaging}
     c.[Id]
    ,c.[ParentId]
    ,c.[Name]
    ,c.[Description]
    ,count(*) over() as [Total]
from [new-keeper].[Categories] as c
where
    
    --{Ids - start}
    c.[Id] in ({Ids}) and
    --{Ids - end}
    
    --{Name - start}
    lower(c.[Name]) like lower(N'%' + @Name + '%') and
    --{Name - end}
    
    1 = 1
    order by c.[Id] desc
{offsetPaging}
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

                query = SqlQueriesFormater.Page("topPaging", "offsetPaging")
                    .Top(Count).Offset(Start)
                    .Format(query);
                
                query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(", ", x)).Format(query);

                if (string.IsNullOrWhiteSpace(Name))
                    query = SqlQueriesFormater.RemoveSubString(query, "Name");

                return query;
            }
        }
    }
}