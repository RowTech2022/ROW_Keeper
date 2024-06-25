using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Organization
    {
        public class List(params int[] ids)
        {
            public int[] Ids { get; set; } = ids;

            public class Result : BaseProp
            {
                [Bind("Id")]
                public int Id { get; set; }

                [Bind("PlanId")]
                public int? PlanId { get; set; }

                [NVarChar("PlanName", 500)]
                public string? PlanName { get; set; }
                
                [Bind("OwnerId")]
                public int OwnerId { get; set; }
                
                [NVarChar("OrgName", 500)]
                public string OrgName { get; set; } = null!;
                
                [NVarChar("OrgDescription", 500)]
                public string? OrgDescription { get; set; } = null!;
                
                [NVarChar("OrgPhone", 20)]
                public string OrgPhone { get; set; } = null!;
                
                [NVarChar("OrgEmail", 20)]
                public string? OrgEmail { get; set; }
                
                [NVarChar("OrgAddress", 500)]
                public string OrgAddress { get; set; } = null!;

                [NVarChar("OwnerFullName", 100)]
                public string OwnerFullName { get; set; } = null!;

                [NVarChar("OwnerEmail", 100)]
                public string? OwnerEmail { get; set; } = null!;

                [NVarChar("OwnerPhone", 20)]
                public string OwnerPhone { get; set; } = null!;

                [Bind("Status")]
                public Client.Organization.OrgStatus Status { get; set; }
            }

            #region c_query

            private const string Query = @"
select distinct 
     o.[Id]
    ,p.[Id] as [PlanId]
    ,p.[Name] as [PlanName]
    ,o.[OwnerId]
    ,o.[OrgName]
    ,o.[OrgDescription]
    ,o.[OrgPhone]
    ,o.[OrgEmail]
    ,o.[OrgAddress]
    ,o.[OwnerFullName]
    ,o.[OwnerEmail]
    ,o.[OwnerPhone]
    ,o.[Status]
    ,o.[CreatedAt]
    ,o.[UpdatedAt]
    ,o.[Timestamp]
from [new-keeper].[Organizations] as o
left join [new-keeper].[Subscriptions] as s on o.[Id] = s.[OrgId] and s.EndDate > getutcdate()
left join [new-keeper].[Plans] as p on s.[PlanId] = p.[Id]
where
    
    --{Ids - start}
    o.[Id] in ({Ids}) and
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
                var query = Query;

                query = SqlQueriesFormater.RemoveOrReplace("Ids", Ids, x => string.Join(", ", x))
                    .Format(query);

                return query;
            }
        }
    }
}