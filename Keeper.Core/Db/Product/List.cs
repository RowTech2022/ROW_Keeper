using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Product
    {
        public class List(params int[] ids)
        {
            public int[]? Ids { get; set; } = ids;
            
            [BindStruct]
            public class Result : BaseProp
            {
                [Bind("Id")]
                public int Id { get; set; }

                [Bind("BranchId")]
                public int BranchId { get; set; }

                [Bind("SupplierId")]
                public int SupplierId { get; set; }

                [NVarChar("CategoryName", 300)] 
                public string CategoryName { get; set; } = null!;

                [Bind("TaxId")]
                public int TaxId { get; set; }
    
                [NVarChar("UPC", 100)]
                public string UPC { get; set; } = null!;
    
                [NVarChar("Name", 500)]
                public string Name { get; set; } = null!;

                [Bind("AgeLimit")]
                public int AgeLimit { get; set; }

                [Bind("Quantity")]
                public int Quantity { get; set; }

                [Bind("BuyingPrice")]
                public decimal BuyingPrice { get; set; }

                [Bind("Price")]
                public decimal Price { get; set; }

                [Bind("TotalPrice")]
                public decimal TotalPrice { get; set; }
    
                [Bind("Margin")]
                public int Margin { get; set; }

                [Bind("ExpiredDate")] 
                public DateTimeOffset? ExpiredDate { get; set; }
            }

            #region c_query

            private const string c_query = @"
select
     p.[Id]
    ,p.[BranchId]
    ,p.[SupplierId]
    ,c.[Name] as [CategoryName]
    ,p.[TaxId]
    ,p.[UPC]
    ,p.[Name]
    ,p.[AgeLimit]
    ,p.[Quantity]
    ,p.[BuyingPrice]
    ,p.[Price]
    ,p.[TotalPrice]
    ,p.[Margin]
    ,p.[ExpiredDate]
    ,p.[CreatedAt]
    ,p.[UpdatedAt]
    ,p.[Timestamp]
from [new-keeper].[Products] as p
join [new-keeper].[Categories] as c on p.[CategoryId] = c.[Id]
where
    
    --{Ids - start}
    p.[Id] in ({Ids}) and
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
                var query = c_query;

                query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(", ", x)).Format(query);

                return query;
            }
        }
    }
}