using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Discount
    {
        public class Search
        {
            public int[]? Ids { get; set; }

            public int? Start { get; set; }
            public int? Count { get; set; }

            public Search()
            { }

            public Search(params int[] ids)
            {
                Ids = ids;
            }

            [BindStruct]
            public class Result
            {
                [Bind("Id")]
                public int Id { get; set; }
            
                [Bind("ProductId")]
                public int? ProductId { get; set; }

                [NVarChar("ProductName", 500)]
                public string? ProductName { get; set; }
            
                [Bind("CategoryId")]
                public int? CategoryId { get; set; }

                [NVarChar("CategoryName", 500)]
                public string? CategoryName { get; set; }
                
                [Bind("Percent")] 
                public double Percent { get; set; }

                [NVarChar("Comment", 5000)] 
                public string? Comment { get; set; }

                [Bind("FromDate")] 
                public DateTimeOffset FromDate { get; set; }

                [Bind("ToDate")] 
                public DateTimeOffset ToDate { get; set; }

                [Bind("Total")]
                public int Total { get; set; }
            }

            #region c_query

            private const string c_query = @"
select
{topPaging}
     d.[Id]
    ,d.[ProductId]
    ,p.[Name] as [ProductName]
    ,d.[CategoryId]
    ,c.[Name] as [CategorName]
    ,d.[Percent]
    ,d.[Comment]
    ,d.[FromDate]
    ,d.[ToDate]
    ,count(*) over() as [Total]
from [new-keeper].[ProductDiscounts] as d
left join [new-keeper].[Products] as p on d.[ProductId] = p.[Id]
left join [new-keeper].[Categories] as c on d.[CategoryId] = c.[Id]
where
    
    --{Ids - start}
    p.Id in ({Ids}) and
    --{Ids - end}
    
    --{UPC - start}
    x.[UPC] like '%' + @UPC + '%' and
    --{UPC - end}
    
    1 = 1
order by p.[Id] desc
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

                return SqlQueriesFormater.RemoveLabels(query);
            }
        }
    }
}