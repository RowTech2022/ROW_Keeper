using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Organization
    {
        [BindStruct]
        public class Search
        {
            public int[]? Ids { get; set; }

            [NVarChar("OrgName", 500)]
            public string? OrgName { get; set; }

            public int? Count { get; set; }
            public int? Start { get; set; }

            [BindStruct]
            public class Result
            {
                [Bind("Id")]
                public int Id { get; set; }

                [Bind("PlanId")]
                public int? PlanId { get; set; }

                [NVarChar("PlanName", 500)]
                public string? PlanName { get; set; }

                [NVarChar("OrgName", 500)]
                public string OrgName  { get; set;} = null!;

                [NVarChar("OrgPhone", 20)]
                public string OrgPhone { get; set;} = null!;

                [Bind("OrgEmail")]
                public string? OrgEmail { get; set;}

                [NVarChar("OrgAddress", 500)] 
                public string OrgAddress { get; set; } = null!;

                [Bind("Status")]
                public Client.Organization.OrgStatus Status { get; set; }
                
                [Bind("Total")]
                public int Total { get; set; }
            }

            #region c_query

            private const string c_query = @"
select
{topPaging}
     o.[Id]
    ,p.[Id] as [PlanId]
    ,p.[Name] as [PlanName]
    ,o.[OrgName]
    ,o.[OrgPhone]
    ,o.[OrgEmail]
    ,o.[OrgAddress]
    ,o.[Status]
    ,count(*) over() as [Total]
from [new-keeper].[Organizations] as o
left join [new-keeper].[Subscriptions] as s on o.[Id] = s.[OrgId] and s.EndDate > getutcdate()
left join [new-keeper].[Plans] as p on s.[PlanId] = p.[Id]
where
    
    --{Ids - start}
    o.[Id] in ({Ids}) and
    --{Ids - end}
    
    --{OrgName - start}
    lower(o.[OrgName]) like lower(N'%' + @OrgName + '%') and
    --{OrgName - end}
    
    o.[Active] = 1 and
    
    1 = 1

order by o.[Id] desc
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

                query = SqlQueriesFormater.RemoveAllNullSections(query, this);

                return query;
            }
        }
    }
}