using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class ProductDiscount
    {
        [BindStruct]
        public class Search
        {
            public int[]? Ids { get; set; }

            [NVarChar("UPC", 300)]
            public string? UPC { get; set; }

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

                [NVarChar("ProductName", 300)]
                public string ProductName { get; set; } = null!;

                [NVarChar("UPC", 100)]
                public string UPC { get; set; } = null!;
                
                [Bind("Percent")] 
                public double Percent { get; set; }

                [NVarChar("Comment", 5000)] 
                public string? Comment { get; set; }

                [Bind("FromDate")] 
                public DateTimeOffset FromDate { get; set; }

                [Bind("ToDate")] 
                public DateTimeOffset ToDate { get; set; }

                [NVarChar("Category",300)]
                public string Category { get; set; } = null!;

                [NVarChar("SubCategory",300)]
                public string SubCategory { get; set; } = null!;

                [Bind("Total")]
                public int Total { get; set; }
            }

            #region c_query

            private const string c_query = @"
select
{topPaging}
     p.[Id]
    ,x.[Name] as [ProductName]
    ,x.[UPC]
    ,p.[Percent]
    ,p.[Comment]
    ,p.[FromDate]
    ,p.[ToDate]
    ,s.[Name] as [SubCategory]
    ,coalesce(c.[Name], s.[Name]) as [Category]
    ,count(*) over() as [Total]
from [new-keeper].[ProductDiscounts] as p
join [new-keeper].[Products] as x on p.[ProductId] = x.[Id]
left join [new-keeper].[Categories] as s on x.[CategoryId] = s.[Id]
left join [new-keeper].[Categories] as c on s.[ParentId] = c.[Id]
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
                
                return sql.Query<Result>(query, this).ToList();
            }

            private string GetQuery()
            {
                var query = c_query;

                query = SqlQueriesFormater.Page("topPaging", "offsetPaging")
                    .Top(Count).Offset(Start)
                    .Format(query);

                query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(", ", x)).Format(query);

                if (string.IsNullOrWhiteSpace(UPC))
                    query = SqlQueriesFormater.RemoveSubString(query, "UPC");

                return SqlQueriesFormater.RemoveLabels(query);
            }
        }
    }
}