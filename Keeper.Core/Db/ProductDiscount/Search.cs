using System.Data;
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
            }

            #region c_query

            private const string c_query = @"
select
     p.[Id]
    ,x.[ProductName]
    ,x.[UPC]
    ,p.[Percent]
    ,p.[Comment]
    ,p.[FromDate]
    ,p.[ToDate]
    ,c.[Name] as [Category]
    ,s.[Name] as [SubCategory]
from [new-keeper].[ProductDiscounts] as p
join [new-keeper].[Product] as x on p.[ProductId] = x.[Id]
join [new-keeper].[Categories] as c on x.[CategoryId] = c.[Id]
join [new-keeper].[Categories] as s on x.[CategoryId] = s.[ParentId]
where
    
    --{Ids - start}
    p.Id in ({Ids}) and
    --{Ids - end}
    
    --{UPC - start}
    x.[UPC] like '%' + @UPC + '%' and
    --{UPC - end}
    
    1 = 1
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

                query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(", ", x)).Format(query);

                if (string.IsNullOrWhiteSpace(UPC))
                    query = SqlQueriesFormater.RemoveSubString("UPC", query);

                return SqlQueriesFormater.RemoveLabels(query);
            }
        }
    }
}