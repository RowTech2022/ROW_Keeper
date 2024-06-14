using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Category
    {
        [BindStruct]
        public class SearchSubCategory
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
    
                [NVarChar("CategoryName", 300)]
                public string CategoryName { get; set; } = null!;

                [NVarChar("SubCategoryName", 300)] 
                public string SubCategoryName { get; set; } = null!;

                [Bind("Total")]
                public int Total { get; set; }
            }

            #region c_query

            private const string c_query = @"
select
{topPaging}
     s.[Id]
    ,c.[Name] as [CategoryName]
    ,s.[Name] as [SubCategoryName]
    ,count(*) over() as [Total]
from [new-keeper].[Categories] as c
join [new-keeper].[Categories] as s on c.[Id] = s.[ParentId]
where
    
    --{Ids - start}
    s.[ParentId] in ({Ids}) and
    --{Ids - end}
    
    --{Name - start}
    lower(s.[Name]) like lower(N'%' + @Name + '%') and
    --{Name - end}
    
    1 = 1
    order by c.[Id] desc
{offsetPaging}
";

            #endregion

            public List<Result> Exec(ISqlExecutor sql)
            {
                var query = GetQuery();

                return sql.Query<Result>(query, this).ToList();
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