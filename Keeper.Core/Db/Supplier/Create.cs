using System.Data;
using Bibliotekaen.Sql;
using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

public partial class Db
{
    public partial class Supplier
    {
        [BindStruct]
        public class Create
        {
            [Bind("ReqUserId")]
            public int ReqUserId { get; set; }

            [Bind("OrgId")]
            public int OrgId { get; set; }
            
            [NVarChar("CompanyName", 500)]
            public string CompanyName { get; set; } = null!;

            [NVarChar("Phone", 20)]
            public string Phone { get; set; } = null!;

            [NVarChar("Email", 100)]
            public string? Email { get; set; }

            [NVarChar("Address", 300)]
            public string? Address { get; set; }

            [Bind("ResultId", Direction = ParameterDirection.Output)]
            public int ResultId { get; set; }

            #region c_query

            private const string c_query = @"
declare @now datetimeoffset(7) = getutcdate()

insert into [new-keeper].[Suppliers] (
     [ReqUserId]
    ,[OrgId]
    ,[CompanyName]
    ,[Phone]
    ,[Email]
    ,[Address]
    ,[CreatedAt]
    ,[UpdatedAt]
    )
select
    @ReqUserId,
    @OrgId,
    @CompanyName,
    @Phone,
    @Email,
    @Address,
    @now,
    @now

set @ResultId = @identity
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