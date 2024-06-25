using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Plan
    {
        public class List(params int[] ids)
        {
            public int[] Ids { get; set; } = ids;
            
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
            }

            #region c_query

            private const string c_query = @"
select 
     p.[Id]
    ,p.[Name]
    ,p.[Price]
    ,p.[Duration]
    ,p.[Type]
    ,p.[CreatedAt]
    ,p.[UpdatedAt]
    ,p.[Timestamp]
from [new-keeper].[Plans] as p
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

                return SqlQueriesFormater.RemoveLabels(query);
            }
        }
    }
}