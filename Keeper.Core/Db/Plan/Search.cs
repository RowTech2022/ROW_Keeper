using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Plan
    {
        [BindStruct]
        public class Search
        {
            public int[]? Ids { get; set; }

            [NVarChar("Name", 100)]
            public string? Name { get; set; }

            public int? Count { get; set; }
            public int? Start { get; set; }
            
            [BindStruct]
            public class Result : BaseProp
            {
                [Bind("Id")] 
                public int Id { get; set; }

                [NVarChar("Name", 500)] 
                public string Name { get; set; } = null!;

                [Bind("Price")] 
                public decimal Price { get; set; }

                [Bind("Duration")] 
                public int Duration { get; set; }

                [Bind("Type")]
                public Client.Plan.PlanType Type { get; set; }

                [Bind("Total")]
                public int Total { get; set; }
            }

            #region c_query

            private const string c_query = @"
select 
{topPaging}
     p.[Id]
    ,p.[Name]
    ,p.[Price]
    ,p.[Duration]
    ,p.[Type]
    ,p.[CreatedAt]
    ,p.[UpdatedAt]
    ,p.[Timestamp]
    ,count(*) over() as [Total]
from [new-keeper].[Plans] as p
where

    --{Ids - start}
    p.[Id] in ({Ids}) and
    --{Ids - end}
    
    --{Name - start}
    lower(p.[Name]) like lower(N'%' + @Name + '%') and
    --{Name - end}
    
    1 = 1

order by p.[Id]
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

                return SqlQueriesFormater.RemoveLabels(query);
            }
        }
    }
}