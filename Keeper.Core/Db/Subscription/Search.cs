using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Subscription
    {
        [BindStruct]
        public class Search
        {
            public int[]? Ids { get; set; }

            public string? OrgName { get; set; }

            public int? Count { get; set; }
            public int? Start { get; set; }
            
            [BindStruct]
            public class Result
            {
                [Bind("Id")]
                public int Id { get; set; }
            
                [Bind("OrgId")]
                public int OrgId { get; set; }

                [NVarChar("OrgName", 500)]
                public string OrgName { get; set; } = null!;
            
                [Bind("PlanId")]
                public int PlanId { get; set; }

                [NVarChar("PlanName", 500)]
                public string PlanName { get; set; } = null!;
            
                [Bind("StartDate")]
                public DateTimeOffset StartDate { get; set; }
            
                [Bind("EndDate")] 
                public DateTimeOffset EndDate { get; set; }

                [Bind("Total")]
                public int Total { get; set; }
            }

            #region c_qeury

            private const string c_query = @"
select
{topPaging}
     s.[Id]
    ,s.[OrgId]
    ,o.[OrgName]
    ,s.[PlanId]
    ,p.[Name] as [PlanName]
    ,s.[StartDate]
    ,s.[EndDate]
    ,count(*) over() as [Total]
from [new-keeper].[Subscriptions] as s
join [new-keeper].[Organizations] as o on s.[OrgId] = o.[Id]
join [new-keeper].[Plans] as p on s.[PlanId] = p.[Id]
where
    
    --{Ids - start}
    s.[Id] in ({Ids}) and
    --{Ids - end}
    
    --{OrgName - start}
    lower(o.[OrgName]) like lower(N'%' + @Name + '%') and
    --{OrgName - end}
    
    1 = 1
order by s.[Id] desc
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

                if (string.IsNullOrWhiteSpace(OrgName))
                    query = SqlQueriesFormater.RemoveSubString(query, "OrgName");

                return SqlQueriesFormater.RemoveLabels(query);
            }
        }
    }
}