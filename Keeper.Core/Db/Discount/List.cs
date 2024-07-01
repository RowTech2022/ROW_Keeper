using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;
using Keeper.Client;

namespace Keeper.Core;

public partial class Db
{
    public partial class Discount
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
                
                [Bind("Type")] 
                public DiscountType Type { get; set; }
            }

            #region c_query

            private const string c_query = @" 
select
     d.[Id]
    ,d.[ProductId]
    ,p.[Name] as [ProductName]
    ,d.[CategoryId]
    ,c.[Name] as [CategorName]
    ,d.[Percent]
    ,d.[Comment]
    ,d.[FromDate]
    ,d.[ToDate]
    ,d.[Type]
    ,d.[CreatedAt]
    ,d.[UpdatedAt]
    ,d.[Timestamp]
from [new-keeper].[Discounts] as d
left join [new-keeper].[Products] as p on d.[ProductId] = p.[Id]
left join [new-keeper].[Categories] as c on d.[CategoryId] = c.[Id]
where
    
    --{Ids - start}
    p.Id in ({Ids}) and
    --{Ids - end}
    
    p.[Active] = 1 and
    
    1 = 1
";

            #endregion

            public List<Result> Ecec(ISqlExecutor sql)
            {
                var query = GetQuery();
                
                return sql.Query<Result>(query, this).ToList();
            }

            private string GetQuery()
            {
                var query = c_query;

                query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(", ", x)).Format(query);

                return SqlQueriesFormater.RemoveLabels(query);
            }
        }
    }
}