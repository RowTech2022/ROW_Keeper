using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core.Product;

public partial class Db
{
    public partial class Product
    {
        public class Search
        {
            [NVarChar("Filter", 500)]
            public string? Filter { get; set; }
            
            public int? Start { get; set; }
            public int? Count { get; set; }
            
            public class Result
            {
                [Bind("Id")]
                public int Id { get; set; }

                [NVarChar("CategoryName", 300)] 
                public string CategoryName { get; set; } = null!;
    
                [NVarChar("UPC", 100)]
                public string UPC { get; set; } = null!;
    
                [NVarChar("Name", 500)]
                public string Name { get; set; } = null!;

                [Bind("AgeLimit")]
                public int AgeLimit { get; set; }

                [Bind("Quantity")]
                public int Quantity { get; set; }

                [Bind("Price")]
                public decimal Price { get; set; }

                [Bind("DiscountPrice")]
                public decimal DiscountPrice { get; set; }

                [Bind("HaveDiscount")]
                public bool HaveDiscount { get; set; }

                [Bind("ExpiredDate")] 
                public DateTimeOffset? ExpiredDate { get; set; }
            }

            #region c_query

            private const string c_query = @"
select
{topPaging}
     p.[Id]
    ,c.[Name] as [CategoryName]
    ,p.[UPC]
    ,p.[Name]
    ,p.[AgeLimit]
    ,p.[Quantity]
    ,p.[Price]
    ,p.[DiscountPrice]
    ,p.[HaveDiscount]
    ,p.[ExpireDate]
from [new-keeper].[Products] as p
join [new-keeper].[Categories] as c on p.[CategoryId] = c.[Id]
where
    
    --{Ids - start}
    p.[Id] in ({Ids}) and
    --{Ids - end}
    
    --{Filter - start}
    (p.[UPC] = @Filter or lower(p.[Name]) like lower(N'%' + @Filter + '%')) and
    --{Filter - end}
    
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
                
                if (string.IsNullOrWhiteSpace(Filter))
                    query = SqlQueriesFormater.RemoveSubString(query, "Filter");
                
                return query;
            }
        }
    }
}