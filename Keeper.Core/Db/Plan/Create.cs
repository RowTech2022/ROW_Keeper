using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Plan
    {
        [BindStruct]
        public class Create
        {
            [Bind("ReqUserId")]
            public int ReqUserId { get; set; }

            [NVarChar("Name", 500)]
            public string Name { get; set; } = null!;

            [Bind("Price")]
            public decimal Price { get; set; }

            [Bind("Duration")]
            public int Duration { get; set; }

            [Bind("Type")]
            public Client.Plan.PlanType Type { get; set; }

            [Bind("ResultId", Direction = ParameterDirection.Output)]
            public int ResultId { get; set; }

            #region c_query

            private const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

insert into [new-keeper].[Plans] (
     [ReqUserId]
    ,[Name]
    ,[Price]
    ,[Duration]
    ,[Type]
    ,[CreatedAt]
    ,[UpdatedAt] )
select
    @ReqUserId,
    @Name,
    @Price,
    @Duration,
    @Type,
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