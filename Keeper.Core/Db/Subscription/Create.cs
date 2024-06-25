using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Subscription
    {
        [BindStruct]
        public class Create
        {
            [Bind("ReqUserId")]
            public int ReqUserId { get; set; }
            
            [Bind("OrgId")]
            public int OrgId { get; set; }
            
            [Bind("PlanId")]
            public int PlanId { get; set; }
            
            [Bind("StartDate")]
            public DateTimeOffset StartDate { get; set; }
            
            [Bind("EndDate")] 
            public DateTimeOffset EndDate { get; set; }

            [Bind("ResultId")]
            public int ResultId { get; set; }

            #region c_query

            private const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()
 
insert into [new-keeper].[Subscriptions] (
     [ReqUserId]
    ,[OrgId]
    ,[PlanId]
    ,[StartDate]
    ,[EndDate]
    ,[CreatedAt]
    ,[UpdatedAt] )
select
    @ReqUserId,
    @OrgId,
    @PlanId,
    @StartDate,
    @EndDate,
    @now,
    @now

set @ResultId = @@identity
";

            #endregion

            public int Exec(ISqlExecutor sql)
            {
                sql.Query(c_query, this);

                return ResultId;
            }
        }
    }
}