using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Product
    {
        [BindStruct]
        public class Search
        {
            public int[]? Ids { get; set; }
            
            [NVarChar("NameOrUPC", 500)]
            public string? NameOrUPC { get; set; }
            public int[]? CategoryIds { get; set; }

            [Bind("OrgId")]
            public int OrgId { get; set; }
            
            public int? Start { get; set; }
            public int? Count { get; set; }
            
            [BindStruct]
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

                [Bind("ExpiredDate")] 
                public DateTimeOffset? ExpiredDate { get; set; }

                [Bind("Total")]
                public int Total { get; set; }
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
    ,p.[ExpiredDate]
    ,count(*) over() as [Total]
from [new-keeper].[Products] as p
join [new-keeper].[Categories] as c on p.[CategoryId] = c.[Id]
where

    p.[OrgId] = @OrgId and
    
    --{Ids - start}
    p.[Id] in ({Ids}) and
    --{Ids - end}
    
    --{Filter - start}
    (p.[UPC] = @NameOrUPC or lower(p.[Name]) like lower(N'%' + @NameOrUPC + '%')) and
    --{Filter - end}
    
    --{CategoryIds - start}
    p.[CategoryId] in ({CategoryIds}) and
    --{CategoryIds - end}
    
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
                
                query = SqlQueriesFormater.RemoveOrReplace("CategoryIds", CategoryIds, x => string.Join(", ", x)).Format(query);
                
                if (string.IsNullOrWhiteSpace(NameOrUPC))
                    query = SqlQueriesFormater.RemoveSubString(query, "Filter");
                
                return SqlQueriesFormater.RemoveLabels(query);
            }
        }
    }
}